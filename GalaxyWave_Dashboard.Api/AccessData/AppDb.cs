using Microsoft.EntityFrameworkCore;
using GalaxyWave_Dashboard.Api.Models.UsersDashboards;
using GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements;

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
