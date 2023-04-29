using AspNetCore;
using Galaxy.Core.Models.AdminSite.Role;
using Galaxy.Data.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Main.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<IdentityUser>user,SignInManager<IdentityUser>signIn, RoleManager<IdentityRole> role )
        {
            userManager = user;
            signInManager = signIn;
            roleManager = role;
        }
        #region Role

        // create , edit , delete 
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole (CreateRole role)
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
            if (ModelState.IsValid)
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
        public async Task<IActionResult>DeleteRole(EditeRole role)
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
        // create account 
        #endregion


    }
}
