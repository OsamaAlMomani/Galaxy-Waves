using Microsoft.AspNetCore.Mvc;

namespace GalaxyWaves.Areas.Administrator.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
