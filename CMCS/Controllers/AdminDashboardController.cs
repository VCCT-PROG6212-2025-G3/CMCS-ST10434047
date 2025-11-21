using CMCS.Data;
using CMCS.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CMCS.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class AdminDashboardController : Controller
    {
        private readonly IDataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminDashboardController(IDataContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var claims = await _context.Claims.Include(c => c.User).ToListAsync();
            var lecturers = await _userManager.GetUsersInRoleAsync("Lecturer");

            var lecturerStats = new List<LecturerStats>();

            foreach (var lecturer in lecturers)
            {
                var lecturerClaims = claims.Where(c => c.UserId == lecturer.Id).ToList();
                if (lecturerClaims.Any())
                {
                    var totalClaims = lecturerClaims.Count;
                    var approvedClaims = lecturerClaims.Count(c => c.Status == ClaimStatus.Approved);

                    lecturerStats.Add(new LecturerStats
                    {
                        LecturerName = $"{lecturer.FirstName} {lecturer.LastName}",
                        TotalClaimValue = lecturerClaims.Sum(c => c.Amount),
                        ClaimsSubmitted = totalClaims,
                        ApprovalRate = totalClaims > 0 ? (double)approvedClaims / totalClaims * 100 : 0
                    });
                }
            }

            var monthlyClaims = claims
            .GroupBy(c => new { c.SubmissionDate.Year, c.SubmissionDate.Month })
            .Select(g => new
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Count = g.Count()
            })
            .OrderBy(x => x.Year).ThenBy(x => x.Month)
            .ToList();

            var viewModel = new AdminDashboardViewModel
            {
                PendingClaims = claims.Count(c => c.Status == ClaimStatus.Pending),
                ApprovedClaims = claims.Count(c => c.Status == ClaimStatus.Approved),
                RejectedClaims = claims.Count(c => c.Status == ClaimStatus.Rejected),
                TopLecturers = lecturerStats.OrderByDescending(l => l.TotalClaimValue).Take(5).ToList(),
                ChartLabels = monthlyClaims.Select(mc => new DateTime(mc.Year, mc.Month, 1).ToString("MMM yyyy", CultureInfo.InvariantCulture)).ToList(),
                ChartData = monthlyClaims.Select(mc => mc.Count).ToList()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "HR")]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                userViewModels.Add(new UserViewModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email ?? "",
                    HourlyRate = user.HourlyRate,
                    Roles = await _userManager.GetRolesAsync(user)
                });
            }
            return View(userViewModels);
        }

        [HttpPost]
        [Authorize(Roles = "HR")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRate(string userId, string hourlyRate)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (decimal.TryParse(hourlyRate.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsedRate))
            {
                user.HourlyRate = parsedRate;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["Success"] = $"Rate updated to R{parsedRate:F2}";
                }
                else
                {
                    TempData["Error"] = "Database update failed.";
                }
            }
            else
            {
                TempData["Error"] = "Invalid number format.";
            }

            return RedirectToAction(nameof(ManageUsers));
        }


        public async Task<IActionResult> EditUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserRolesViewModel
            {
                UserId = user.Id,
                UserName = $"{user.FirstName} {user.LastName}",
                Roles = new List<RoleSelection>()
            };

            foreach (var role in _roleManager.Roles.ToList())
            {
                model.Roles.Add(new RoleSelection
                {
                    RoleName = role.Name,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                });
            }

            return View(model);
        }

        // POST: Updates the roles for a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserRoles(EditUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            if (!result.Succeeded)
            {
                // Handle error
                return View(model);
            }

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded)
            {
                // Handle error
                return View(model);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        // GET: Displays all roles
        public async Task<IActionResult> ManageRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        // GET: View claims waiting for final approval
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewClaims()
        {
            var claims = await _context.Claims
                .Include(c => c.User)
                .Where(c => c.Status == ClaimStatus.Verified)
                .OrderBy(c => c.SubmissionDate)
                .ToListAsync();

            return View(claims);
        }

        // POST: Final Approval
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveClaim(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved; // Final Status
                await _context.SaveChangesAsync();
                TempData["Success"] = "Claim approved successfully.";
            }
            return RedirectToAction(nameof(ViewClaims));
        }

        // POST: Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectClaim(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Claim rejected.";
            }
            return RedirectToAction(nameof(ViewClaims));
        }

        // GET: HR Reports Page
        [Authorize(Roles = "HR")]
        public IActionResult Reports()
        {
            return View();
        }

        // POST: Generate PDF Report
        [HttpPost]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> GenerateReport(int month, int year)
        {
            // 1. Fetch Data using LINQ
            var claims = await _context.Claims
                .Include(c => c.User)
                .Where(c => c.Status == ClaimStatus.Approved &&
                            c.SubmissionDate.Month == month &&
                            c.SubmissionDate.Year == year)
                .OrderBy(c => c.SubmissionDate)
                .ToListAsync();

            // 2. Create PDF in Memory
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                var titleText = new Text("Monthly Approved Claims Report")
                    .SetFontSize(20).SimulateBold();

                document.Add(new Paragraph(titleText)
                    .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph($"Period: {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}")
                    .SetFontSize(12)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20));

                if (claims.Any())
                {
                    // Table Setup
                    var table = new Table(5).UseAllAvailableWidth();
                    table.AddHeaderCell("Claim ID");
                    table.AddHeaderCell("Lecturer");
                    table.AddHeaderCell("Date");
                    table.AddHeaderCell("Hours");
                    table.AddHeaderCell("Total Amount");

                    decimal totalPayout = 0;

                    // Rows
                    foreach (var claim in claims)
                    {
                        table.AddCell($"#{claim.Id}");
                        table.AddCell($"{claim.User.FirstName} {claim.User.LastName}");
                        table.AddCell(claim.SubmissionDate.ToString("yyyy-MM-dd"));
                        table.AddCell(claim.HoursWorked.ToString("0.##"));
                        table.AddCell(claim.Amount.ToString("C"));
                        totalPayout += claim.Amount;
                    }

                    document.Add(table);
                    var totalText = new Text($"\nTotal Payout: {totalPayout.ToString("C")}")
                        .SetFontSize(14)
                        .SimulateBold();

                    document.Add(new Paragraph(totalText)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetMarginTop(10));
                }
                else
                {
                    document.Add(new Paragraph("No approved claims found for this period."));
                }

                document.Close();

                // 3. Return File
                return File(stream.ToArray(), "application/pdf", $"Claims_Report_{month}_{year}.pdf");
            }
        }
    }
}