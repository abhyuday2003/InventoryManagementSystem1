using InventoryManagementSystem1.AppData;
using InventoryManagementSystem1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InventoryManagementSystem1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

                if (user != null)
                {
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "AdminDashboard");
                    }
                    else if (user.Role == "Customer")
                    {
                        return RedirectToAction("CustomerDashboard", "Users");
                    }
                }

                TempData["Error"] = "Invalid username or password.";
            }

            return View(model);
        }


        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Email = model.Email,
                    Role = model.Role, // Default Role as Customer
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["Success"] = "Registration successful! You can now log in.";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "Registration failed. Please check your details.";
            return View(model);
        }

        // GET: Logout
        [HttpGet]
        public IActionResult Logout()
        {
            Console.WriteLine("Logout action is called.");
            HttpContext.SignOutAsync();

            TempData["Success"] = "You have been logged out successfully.";
            return RedirectToAction("Index", "Home");
        }


    }
}
