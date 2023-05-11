using Azure.Core;
using Main.Areas.Admin.Models.RoleViewModel;
using Main.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Roles_UsersController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        private readonly AppDbContext db;
        public Roles_UsersController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser>userManager,AppDbContext _db)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            db = _db;
        }
        #region Roles Manager
                // ----------*Get Methods*------------
        public async Task<IActionResult> Index()  // = > List of Roles  
        {
            return View(roleManager.Roles);
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            if (Id == null)
            {
                return View("Error");
            }
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return View("Error");
            }
            EditRoleViewModel model = new EditRoleViewModel { RoleName = role.Name };
            return View(model);
        }
        public async Task< IActionResult> DeleteRole(string id)
        {

            if (id == null)
            {
                return View("Error");
            }
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("Error");
            }
            EditRoleViewModel model = new EditRoleViewModel { RoleName = role.Name };
            return View(model);
        }
        //--------*End of Get Methods*-------------

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                    IdentityRole identity = new IdentityRole
                    {
                        Name = model.RoleName
                    };
                    var result = await roleManager.CreateAsync(identity);
                    if (result.Succeeded)
                    {
                    
                        return RedirectToAction("Index");
                    }
                
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
       


        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    return View("Error");
                }


                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(EditRoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var find = await roleManager.FindByIdAsync(model.Id);
                if (find != null)
                {
                  
                    var update = await roleManager.DeleteAsync(find);
                    if (update.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region User Maneger
        [HttpGet]
        public IActionResult UserRole()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GiveUser_Role()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> GiveUser_Role(user)
        //{
        //    return View();
        //}
        #endregion
    }
}
