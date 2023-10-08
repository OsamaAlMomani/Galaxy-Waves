using GalaxyWave.SQL_Model.Models.UsersDashboards.Elements;
using GalaxyWave.UI.Areas.AccessMethod.Models;
using GalaxyWave.UI.DataCenter;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyWave.UI.Areas.AccessMethod.Controllers
{
    public class AccessPoint : Controller
    {
        private readonly appDb db;

        public AccessPoint(appDb _db)
        {
            db= _db;
        }
        public IActionResult SignInPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(SignIn user)
        {
            if (user == null)
            {
                // Error Handler For Empty object 
            }

            if (ModelState.IsValid)
            {
                var Found = db.users.FirstOrDefault(a=>a.UserEmail ==user.Email);
                if (Found != null) {
                    return View(user);
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult RegisterPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {

            return View();
        }

    }
}
