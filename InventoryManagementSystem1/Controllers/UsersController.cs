using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using InventoryManagementSystem1.AppData;
using InventoryManagementSystem1.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InventoryManagementSystem1.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult CustomerDashboard()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Register (Users user)
        {
            if(ModelState.IsValid)
            {
                user.CreatedAt = DateTime.Now;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }

        public IActionResult Login()
        {
            return View();  // Return the login page
        }
        [HttpPost]
        public async Task<IActionResult> Login(Users model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill in all required fields.";
                return RedirectToAction("Index", "Home");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user == null )
            {
                TempData["ErrorMessage"] = "Invalid username, password, or role.";
                return RedirectToAction("Index", "Home");
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.Role)
    };

            // Create claims identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // Store Role and UserId in the session on successful login
            HttpContext.Session.SetString("Role", user.Role); // Storing Role
            HttpContext.Session.SetInt32("UserId", user.UserId); // Storing UserId

            // Redirect based on role
            if (user.Role == "Customer")
                return RedirectToAction("CustomerDashboard", "Customer");
           /* else if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin"); */

            TempData["ErrorMessage"] = "Unknown role.";
            return RedirectToAction("Index", "Home");
        }
        

    }
}
