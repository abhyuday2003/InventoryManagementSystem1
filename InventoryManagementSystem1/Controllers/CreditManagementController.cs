using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem1.Models;
using System.Linq;
using InventoryManagementSystem1.AppData;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem1.Controllers
{
    public class CreditManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch and Display Credit Information
        public IActionResult Details(int userId)
        {
            var creditData = _context.CreditManagement
                .Include(c => c.Users)
                .Include(c => c.Suppliers)
                .Where(c => c.UserId == userId)
                .ToList();

            if (creditData == null  )
            {
                // Redirect to NoCreditInfo view if no credit data exists
                return View("NoCreditInfo");
            }

            return View(creditData);
        }

        // Pay Outstanding Balance
        [HttpPost]
        public IActionResult Pay(int creditId, decimal paymentAmount)
        {
            var creditRecord = _context.CreditManagement
                .FirstOrDefault(c => c.CreditId == creditId);

            if (creditRecord != null)
            {
                creditRecord.OutstandingBalance -= paymentAmount;

                // Prevent negative balance
                if (creditRecord.OutstandingBalance < 0)
                {
                    creditRecord.OutstandingBalance = 0;
                }

                creditRecord.LastUpdated = DateTime.Now;
                _context.SaveChanges();
                TempData["Success"] = "Payment successful.";
            }
            else
            {
                TempData["Error"] = "Credit record not found.";
            }

            return RedirectToAction("Details", new { userId = creditRecord?.UserId });
        }
    }

}
