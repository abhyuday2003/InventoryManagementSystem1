using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem1.AppData;  // Your ApplicationDbContext namespace
using InventoryManagementSystem1.Models;  // Your models namespace
using System.Linq;

namespace InventoryManagementSystem1.Controllers
{
    public class CreditManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /CreditManagement/Details?userId=1
        public IActionResult Details(int userId)
        {
            // Fetch credit information for the user
            var creditInfo = _context.CreditManagement
                .Include(c => c.Suppliers)  // Include supplier details
                .FirstOrDefault(c => c.UserId == userId);

            if (creditInfo == null)
            {
                ViewBag.Message = "No credit information found for this user.";
                return View("NoCreditInfo");
            }

            return View(creditInfo);
        }

        // POST: /CreditManagement/Pay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Pay(int creditId, decimal amount)
        {
            var creditInfo = _context.CreditManagement.FirstOrDefault(c => c.CreditId == creditId);

            if (creditInfo == null)
            {
                return NotFound("Credit record not found.");
            }

            if (amount <= 0)
            {
                ModelState.AddModelError("", "Invalid payment amount.");
                return View("Details", creditInfo);
            }

            // Deduct payment from outstanding balance
            creditInfo.OutstandingBalance -= amount;

            // Ensure outstanding balance doesn't go negative
            if (creditInfo.OutstandingBalance < 0)
            {
                creditInfo.OutstandingBalance = 0;
            }

            creditInfo.LastUpdated = DateTime.Now;
            _context.SaveChanges();

            ViewBag.Message = "Payment successful!";
            return RedirectToAction("Details", new { userId = creditInfo.UserId });
        }
    }
}
