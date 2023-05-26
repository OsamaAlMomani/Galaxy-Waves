using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class MaintenanceController : Controller
    {
        public IActionResult maintenance()
        {
            return View();
        }
    }
}
