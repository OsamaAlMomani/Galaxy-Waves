﻿//using GalaxyWave_Dashboard.Api.AccessData;
//using GalaxyWave_Dashboard.Api.Services.Repo.RepoInterface;
//using GalaxyWave_Dashboard.Api.Models.UsersDashboards;
//using GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements;
//using Microsoft.EntityFrameworkCore;
//using NuGet.Protocol;

//namespace GalaxyWave_Dashboard.Api.Services.Repo.RepoClass
//{
//    public class DashboardRepo : IDashboardRepo
//    {
//        private readonly AppDb db;


//        public DashboardRepo(AppDb _db)
//        {
//            db = _db;
//        }



//        public void User_Dashboard_Creation(User user)
//        {

//            Dashboard dashboard = new Dashboard
//            {
//                DashboardId = Guid.NewGuid(),
//                Description = "",
//                CreateTime = DateTime.Now,
//                UpdateTime = DateTime.Now,
//                Name = user.UserName,
//                UserId = user.UserId,
//            };

//            var result = db.dashboards.Add(dashboard);

//            if (result != null)
//            {
//                db.SaveChanges();
//                return;
//            }
//            Console.WriteLine("Error did not Create Dashboard for user : " + dashboard.Name);
//        }

//        public void User_Dashboard_Delete(User user)
//        {

//            var unNeededDashboard = db.dashboards.Include(a => a.UserId).FirstOrDefault(a => a.UserId == user.UserId);
//            var unNeededUser = db.users.FirstOrDefault(a => a.UserId == user.UserId);
//            if (unNeededDashboard != null && unNeededUser != null)
//            {
//                db.dashboards.Remove(unNeededDashboard);
//                db.users.Remove(unNeededUser);
//                db.SaveChanges();
//            }
//        }


//        public void User_Dashboard_Update(User user, Dashboard table)
//        {
//            if (table != null && user != null)
//            {
//                var DashboardIdResult = db.dashboards.Include(a => a.UserId).FirstOrDefault(a => a.DashboardId == table.DashboardId);
//                if (DashboardIdResult != null)
//                {
//                    var result = db.dashboards.Update(table);
//                    if (result != null)
//                    {
//                        db.SaveChanges();
//                    }
//                }
//            }

//        }

//        public void User_Dashboard_View(User user)
//        {
//            var value = db.dashboards.Include(a => a.UserId).ToList();
//            return value;
//        }
//        public void User_Dashboard_Bottom()
//        {
//            throw new NotImplementedException();
//        }

//        public void User_Dashboard_bring(User user, int length, Dashboard dashboard)
//        {
//            throw new NotImplementedException();
//        }

//        public void User_Dashboard_Top()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
