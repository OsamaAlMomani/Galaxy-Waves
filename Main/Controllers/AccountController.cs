using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> signInManager { get; set; }
        public AccountController( SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
           
        }

    }
}
