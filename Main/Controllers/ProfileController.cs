using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
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
            return RedirectToAction("maintenance", "Maintenance");
            //return View();
        }
        public IActionResult Help()
        {
            return View();
        }


    }
}
