using GalaxyWave_Dashboard.Api.Models.UsersDashboards;
using GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements;

namespace GalaxyWave_Dashboard.Api.Services.Repo.RepoInterface
{
    internal interface IDashboardRepo
    {
        public void User_Dashboard_Creation(User user);
        public void User_Dashboard_Delete(User user);
        public void User_Dashboard_Update(User user, Dashboard table);
        public void User_Dashboard_View(User user);
        public void User_Dashboard_bring(User user,int length, Dashboard dashboard);
        public void User_Dashboard_Top();
        public void User_Dashboard_Bottom();

    }
}
