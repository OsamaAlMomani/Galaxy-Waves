using Microsoft.AspNetCore.Mvc;

namespace GalaxyWaves.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult userProfile()
        {
            return View();
        }
        public IActionResult userSetting()
        {
            return View();
        }
        public IActionResult userWallet()
        {
            return RedirectToAction("GalaxyWavestenance", "GalaxyWavestenance");
            //return View();
        }
        public IActionResult Help()
        {
            return View();
        }


    }
}
