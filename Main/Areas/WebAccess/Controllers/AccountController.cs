using Main.Areas.WebAccess.Models.WebAccessModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.WebAccess.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager; 
            this.userManager = userManager; 
        }
        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(SignIn model)
        {
            if (ModelState.IsValid)
            {
                
                var Result = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index", "home");
                }

                ModelState.AddModelError("", "User Invalid");

                return View(model);
            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");


        }
    }
}
