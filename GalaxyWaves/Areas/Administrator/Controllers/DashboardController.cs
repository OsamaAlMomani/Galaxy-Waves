using Microsoft.AspNetCore.Mvc;

namespace GalaxyWaves.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class DashboardController : Controller
    {
        public IActionResult dashboard()
        {
            return View();
        }
    }
}
