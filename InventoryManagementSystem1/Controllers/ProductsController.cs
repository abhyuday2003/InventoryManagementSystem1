using InventoryManagementSystem1.AppData;
using InventoryManagementSystem1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InventoryManagementSystem1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        { 
           // Fetch all products including their category and supplier details
            var products = _context.Products
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    CategoryName = p.Categories.CategoryName,
                    SupplierName = p.Suppliers.SupplierName,
                    p.UnitPrice,
                    p.QuantityStock,
                    p.ReorderLevel
                })
                .ToList();
 
            return View(products);
        }
    }
}
