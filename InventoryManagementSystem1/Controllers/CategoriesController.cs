using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem1.Models;
using InventoryManagementSystem1.AppData;

namespace InventoryManagementSystem1.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Create Category
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categories category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Success"] = "Category added successfully!";
                return RedirectToAction("Index"); // Redirect to a View Categories page
            }

            TempData["Error"] = "Failed to add category. Please try again.";
            return View(category);
        }
    }
}