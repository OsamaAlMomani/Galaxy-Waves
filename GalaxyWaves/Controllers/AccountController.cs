using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyWaves.Controllers
{
    public class AccountController : Controller
    {
        #region Initialize
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser>signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;

        }
        #endregion
        #region Login/SignUp
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        #endregion
        
    }
}
