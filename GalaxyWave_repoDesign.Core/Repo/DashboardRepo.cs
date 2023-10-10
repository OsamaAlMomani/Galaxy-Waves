using GalaxyWave.SQL_Model.Models.UsersDashboards;
using GalaxyWave.SQL_Model.Models.UsersDashboards.Elements;
using GalaxyWave.UI.DataCenter;
using GalaxyWave_repoDesign.Core.RepoInterface;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace GalaxyWave_repoDesign.Core.Repo
{
    public class DashboardRepo : IDashboardRepo
    {
        private readonly appDb db;
        public DashboardRepo(appDb _db)
        {
            db = _db;

        }

        public void User_Dashboard_Creation(User user)
        {

            Dashboard dashboard = new Dashboard
            {
                DashboardId = Guid.NewGuid(),
                Description = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Name = user.UserName,
                UserId = user.UserId,
            };

            var result = db.dashboards.Add(dashboard);

            if (result != null)
            {
                db.SaveChanges();
                return;
            }
            Console.WriteLine("Error did not Create Dashboard for user : " + dashboard.Name);
        }

        public void User_Dashboard_Delete(User user)
        {

            var unNeededDashboard = db.dashboards.Include(a => a.UserId).FirstOrDefault(a => a.UserId == user.UserId);
            var unNeededUser = db.users.FirstOrDefault(a => a.UserId == user.UserId);
            if (unNeededDashboard != null && unNeededUser != null)
            {
                db.dashboards.Remove(unNeededDashboard);
                db.users.Remove(unNeededUser);
                db.SaveChanges();
            }
        }

        public void User_Dashboard_Update(User user, Dashboard table)
        {
            if (table != null && user != null)
            { 
                var DashboardIdResult = db.dashboards.Include(a=>a.UserId).FirstOrDefault(a => a.DashboardId == table.DashboardId);
                if (DashboardIdResult != null)
                {
                    var result = db.dashboards.Update(table);
                    if (result != null)
                    {
                        db.SaveChanges();
                    }
                }
            }

        }

        public void User_Dashboard_View(User user)
        {
                throw new NotImplementedException();
        }
    }
}
