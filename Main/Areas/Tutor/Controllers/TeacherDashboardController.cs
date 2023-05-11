using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Tutor.Controllers
{
    public class TeacherDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
