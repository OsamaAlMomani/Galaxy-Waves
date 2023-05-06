using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn, RoleManager<IdentityRole> role)
        {
            userManager = user;
            signInManager = signIn;
            roleManager = role;
        }
        #region Role

        // create , edit , delete 
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EditRole()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult DeleteRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRole role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole _role = new IdentityRole
                {
                    Name = role.Name,
                };
                var result = await roleManager.CreateAsync(_role);
                if (result.Succeeded)
                {
                    return View(role);
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View(_role);
            }
            return View(role);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditeRole role)
        {
            if (ModelState.IsValid && role.RoleId != null)
            {
                var result = await roleManager.FindByIdAsync(role.RoleId);
                if (result != null)
                    return View(role);
                result.Name = role.RoleName;
                var IsSuccess = await roleManager.UpdateAsync(result);
                if (IsSuccess.Succeeded)
                    return RedirectToAction("RoleList");
                foreach (var item in IsSuccess.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View(role);
            }
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(EditeRole role)
        {
            if (ModelState.IsValid)
            {
                var _role = await roleManager.FindByIdAsync(role.RoleId);
                var result = await roleManager.DeleteAsync(_role);
                if (result.Succeeded)
                    return RedirectToAction("roleList");
                foreach (var item in result.Errors)
                    ModelState.AddModelError(item.Code, item.Description);
                return View(role);
            }
            return View(role);
        }
        #endregion

        #region user 
        // login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> login(LoginViewModel user)
        {
            if (ModelState.IsValid && user.Password != null && user.EmailAddress != null)
            {
                var result = await signInManager.PasswordSignInAsync(user.EmailAddress, user.Password, false, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                return View(user);
            }
            return View(user);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser I_user = new IdentityUser
                {
                    UserName = user.EmailAddress,
                    Email = user.EmailAddress,
                    PasswordHash = user.Password,
                };
                var result = await userManager.CreateAsync(I_user);
                if (result.Succeeded) return RedirectToAction("Index", "Home");
                return View(user);
            }
            return View(user);
        }
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        // create account 
        #endregion


    }
}
