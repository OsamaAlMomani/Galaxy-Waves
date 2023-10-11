using GalaxyWave_Dashboard.Api.Areas.Dashboard_GetUser.Models.UsersDashboards;
using GalaxyWave_Dashboard.Api.Areas.Dashboard_GetUser.Models.UsersDashboards.Elements;



namespace GalaxyWave_Dashboard.Api.Areas.Dashboard_GetUser.Repo.RepoInterface
{
    internal interface IDashboardRepo
    {
        public void User_Dashboard_Creation(User user);
        public void User_Dashboard_Delete(User user);
        public void User_Dashboard_Update(User user, Dashboard table);
        public void User_Dashboard_View(User user);
    }
}
