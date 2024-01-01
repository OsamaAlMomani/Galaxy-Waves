using GalaxyWave_Dashboard.Api.AccessData;
using GalaxyWave_Dashboard.Api.Models.UsersDashboards;
using GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements;
using GalaxyWave_Dashboard.Api.Services.RepoClass;
using GalaxyWave_Dashboard.Api.Utility;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyWave_Dashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AppDb db;
        private readonly RepoDashboard<Dashboard> repoDashboard;
        private readonly UserValidation userValidation;


        public UserProfileController(AppDb _db,
        RepoDashboard<Dashboard> _repoDashboard,
        UserValidation _userValidation)
        {

            db = _db;
            repoDashboard = _repoDashboard;
            userValidation = _userValidation;

        }
        public IActionResult actionResult(User user)
        {
            var IsExist = userValidation.Does_Id_Exist(user); 
            if (IsExist)
            {
                var Dashboard_Data = repoDashboard.User_Dashboard_Profile(user.UserId);
                if (Dashboard_Data != null)
                {
                    return Ok(Dashboard_Data);
                }
            }
            return Ok(404);
            }

    }
}
