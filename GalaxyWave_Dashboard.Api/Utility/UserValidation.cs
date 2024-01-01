using GalaxyWave_Dashboard.Api.AccessData;
using GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements;

namespace GalaxyWave_Dashboard.Api.Utility
{
    public class UserValidation
    {
        private readonly AppDb db;
        public UserValidation(AppDb _db)
        {
            db = _db;
        }
        public bool Does_Id_Exist(User user)
        {
            if (user != null)
            {

                var checking = db.users.FirstOrDefault(a => a.UserId == user.UserId);
                if (checking == null)
                {
                    return false;
                }
                return true;    
            }
            return false;
            
        }
    }
}
