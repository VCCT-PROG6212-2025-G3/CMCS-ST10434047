using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMCS.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class LecturerAppController : Controller
    {
        private readonly IDataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LecturerAppController(IDataContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            var user = await _userManager.GetUserAsync(User);
            var claims = await _context.Claims
                                       .Where(c => c.UserId == user.Id)
                                       .OrderByDescending(c => c.SubmissionDate)
                                       .ToListAsync();

            ViewData["FullName"] = $"{user.FirstName} {user.LastName}";
            ViewData["PendingClaims"] = claims.Count(c => c.Status == ClaimStatus.Pending);
            ViewData["ApprovedClaims"] = claims.Count(c => c.Status == ClaimStatus.Approved);
            ViewData["RejectedClaims"] = claims.Count(c => c.Status == ClaimStatus.Rejected);

            return View(claims.Take(5));
        }

        public async Task<IActionResult> Claims()
        {
            var user = await _userManager.GetUserAsync(User);
            var claims = await _context.Claims
                                       .Where(c => c.UserId == user.Id)
                                       .OrderByDescending(c => c.SubmissionDate)
                                       .ToListAsync();
            return View(claims);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var claim = await _context.Claims
                                      .FirstOrDefaultAsync(m => m.Id == id && m.UserId == user.Id);

            if (claim == null) return NotFound();

            return View(claim);
        }

        [HttpGet]
        public async Task<IActionResult> NewClaim()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new ClaimInputModel
            {
                HourlyRate = user.HourlyRate
            };

            if (model.HourlyRate <= 0)
            {
                ModelState.AddModelError("", "Your hourly rate has not been set by HR. Please contact them.");
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewClaim(ClaimInputModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.HourlyRate = user.HourlyRate;
            ModelState.Remove("HourlyRate");

            // SERVER-SIDE VALIDATION (180 Hours Limit)
            if (model.HoursWorked > 180)
            {
                ModelState.AddModelError("HoursWorked", "You cannot claim more than 180 hours in a month.");
            }

            if (model.HourlyRate <= 0)
            {
                ModelState.AddModelError("", "Your hourly rate has not been set by HR. Please contact support.");
            }

            // FILE UPLOAD HANDLING
            if (model.Document != null)
            {
                var fileSizeLimit = 5 * 1024 * 1024; // 5 MB
                if (model.Document.Length > fileSizeLimit)
                {
                    ModelState.AddModelError("Document", "The file size cannot exceed 5 MB.");
                }

                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var extension = Path.GetExtension(model.Document.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("Document", "Invalid file type. Only .pdf, .docx, and .xlsx are allowed.");
                }
            }

            if (ModelState.IsValid)
            {
                var totalAmount = model.HoursWorked * model.HourlyRate;

                var claim = new Claim
                {
                    UserId = user.Id,
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    Amount = totalAmount,
                    Description = model.Description,
                    AdditionalNotes = model.AdditionalNotes ?? string.Empty,
                    SubmissionDate = DateTime.UtcNow,
                    Status = ClaimStatus.Pending,
                    DocumentPath = ""
                };

                if (model.Document != null && model.Document.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Document.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Document.CopyToAsync(fileStream);
                    }
                    claim.DocumentPath = "/uploads/" + uniqueFileName;
                }

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Claims));
            }

            return View(model);
        }

        public async Task<IActionResult> Reports()
        {
            var user = await _userManager.GetUserAsync(User);
            var claims = await _context.Claims
                                       .Where(c => c.UserId == user.Id)
                                       .OrderBy(c => c.SubmissionDate)
                                       .ToListAsync();

            var totalClaims = claims.Count;
            var approvedClaimsCount = claims.Count(c => c.Status == ClaimStatus.Approved);

            // Group claims by month for the chart
            var monthlyApprovedClaims = claims
                .Where(c => c.Status == ClaimStatus.Approved)
                .GroupBy(c => new { c.SubmissionDate.Year, c.SubmissionDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(c => c.Amount)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            var viewModel = new LecturerReportViewModel
            {
                TotalClaimsSubmitted = totalClaims,
                ApprovedClaims = approvedClaimsCount,
                ApprovalRate = totalClaims > 0 ? (double)approvedClaimsCount / totalClaims * 100 : 0,
                TotalAmountClaimed = claims.Sum(c => c.Amount),
                TotalAmountApproved = claims.Where(c => c.Status == ClaimStatus.Approved).Sum(c => c.Amount),
                AverageClaimAmount = totalClaims > 0 ? claims.Average(c => c.Amount) : 0,
                ChartLabels = monthlyApprovedClaims.Select(mc => new DateTime(mc.Year, mc.Month, 1).ToString("MMM yyyy", CultureInfo.InvariantCulture)).ToList(),
                ChartData = monthlyApprovedClaims.Select(mc => mc.TotalAmount).ToList()
            };

            return View(viewModel);
        }
    }
}