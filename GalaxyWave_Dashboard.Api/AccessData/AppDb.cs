using GalaxyWave_Dashboard.Api.Areas.Dashboard_GetUser.Models.UsersDashboards.Elements;
using GalaxyWave_Dashboard.Api.Areas.Dashboard_GetUser.Models.UsersDashboards;
using Microsoft.EntityFrameworkCore;

namespace GalaxyWave_Dashboard.Api.AccessData
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options){}

        public DbSet<User> users { get; set; }
        public DbSet<Dashboard> dashboards { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<website> websites { get; set; }
        public DbSet<Widget> widgets { get; set; }

    }
}
