using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem1.AppData;  // Your ApplicationDbContext namespace
using System.Linq;

namespace InventoryManagementSystem1.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Reports/UserSummary?userId=1
        public IActionResult UserSummary(int userId)
        {
            // Fetch total orders by user
            var totalOrders = _context.Orders
                .Where(o => o.UserId == userId)
                .Count();

            // Fetch total products purchased by user
            var totalProductsPurchased = _context.OrderDetails
                .Include(od => od.Orders)
                .Where(od => od.Orders.UserId == userId)
                .Sum(od => od.Quantity);

            // Fetch total amount spent by user
            var totalAmountSpent = _context.Orders
                .Where(o => o.UserId == userId)
                .Sum(o => o.TotalAmount);

            // Create a report summary object to pass to the view
            var reportSummary = new
            {
                TotalOrders = totalOrders,
                TotalProductsPurchased = totalProductsPurchased,
                TotalAmountSpent = totalAmountSpent
            };

            return View(reportSummary);
        }
    }
}
