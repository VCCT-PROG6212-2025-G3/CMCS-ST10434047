using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Controllers
{
    [Authorize(Roles = "ProgramCoordinator")]
    public class CoordinatorAppController : Controller
    {
        private readonly IDataContext _context;

        public CoordinatorAppController(IDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var claims = await _context.Claims.Include(c => c.User).Where(c => c.Status == ClaimStatus.Pending).OrderByDescending(c => c.SubmissionDate).ToListAsync();

            // Stats for the top cards
            ViewData["TotalPending"] = claims.Count;
            ViewData["TotalVerified"] = await _context.Claims.CountAsync(c => c.Status == ClaimStatus.Verified);
            ViewData["TotalRejected"] = await _context.Claims.CountAsync(c => c.Status == ClaimStatus.Rejected);

            return View(claims);
        }

        // New action to show claim details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims
                                      .Include(c => c.User) // Include user details
                                      .FirstOrDefaultAsync(m => m.Id == id);

            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int claimId, ClaimStatus newStatus)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = newStatus;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reports()
        {
            var claims = await _context.Claims.ToListAsync();

            var totalClaims = claims.Count;
            var verifiedClaims = claims.Count(c => c.Status == ClaimStatus.Verified);
            var approvedClaims = claims.Count(c => c.Status == ClaimStatus.Approved);
            var rejectedClaims = claims.Count(c => c.Status == ClaimStatus.Rejected);

            var chartLabels = new List<string> { "Pending", "Verified", "Approved", "Rejected" };
            var chartData = new List<decimal> {
                claims.Count(c => c.Status == ClaimStatus.Pending),
                verifiedClaims,
                approvedClaims,
                rejectedClaims
            };

            var viewModel = new LecturerReportViewModel
            {
                TotalClaimsSubmitted = totalClaims,
                ApprovedClaims = verifiedClaims,
                ApprovalRate = totalClaims > 0 ? (double)verifiedClaims / totalClaims * 100 : 0,
                TotalAmountClaimed = claims.Sum(c => c.Amount),
                TotalAmountApproved = claims.Where(c => c.Status == ClaimStatus.Verified || c.Status == ClaimStatus.Approved).Sum(c => c.Amount),
                ChartLabels = chartLabels,
                ChartData = chartData
            };

            return View(viewModel);
        }
    }
}