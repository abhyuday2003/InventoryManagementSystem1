using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem1.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult CustomerDashboard()
        {
            return View();
        }
    }

}
