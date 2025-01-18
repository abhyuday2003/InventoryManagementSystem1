using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // For Include()
using InventoryManagementSystem1.AppData;
using InventoryManagementSystem1.Models;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManagementSystem1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Orders/Create
        public IActionResult Create()
        {
            // Fetch all products for the dropdown in the view
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST: /Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Orders order, List<int> ProductIds, List<int> Quantities)
        {

            if (ModelState.IsValid)
            {
                // Create a new order
                order.OrderDate = DateTime.Now;
                order.Status = "Pending";
                _context.Orders.Add(order);
                _context.SaveChanges();  // Save order to get OrderId

                // Add each product to OrderDetails table
                for (int i = 0; i < ProductIds.Count; i++)
                {
                    var product = _context.Products.FirstOrDefault(p => p.ProductId == ProductIds[i]);

                    if (product != null)
                    {
                        var orderDetail = new OrderDetails
                        {
                            OrderId = order.OrderId,
                            ProductId = ProductIds[i],
                            Quantity = Quantities[i],
                            Price = product.UnitPrice * Quantities[i]
                        };
                        decimal Totalbill = product.UnitPrice * Quantities[i];
                        order.TotalAmount = Totalbill;
                        _context.Update(order);
                        // Update stock after order placement
                        product.QuantityStock -= Quantities[i];
                        _context.OrderDetails.Add(orderDetail);
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction("OrderSuccess");
            }

            ViewBag.Products = _context.Products.ToList();
            return View(order);
        }

        // Success Page after placing an order
        public IActionResult OrderSuccess()
        {
            return View();
        }
        // GET: /Orders/History
        public IActionResult History(int userId)
        {
            // Fetch the order history for the specific user
            var orders = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Products)  // Include related products in the order details
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)  // Display recent orders first
                .ToList();

            return View(orders);
        }
    }
}