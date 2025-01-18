using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem1.Controllers
{
    public class loginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
