using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem1.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View(); 
        }
    }

}
