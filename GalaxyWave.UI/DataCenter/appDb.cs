using GalaxyWave.SQL_Model.Models.UsersDashboards;
using GalaxyWave.SQL_Model.Models.UsersDashboards.Elements;
using Microsoft.EntityFrameworkCore;

namespace GalaxyWave.UI.DataCenter
{
    public class appDb:DbContext
    {
        public appDb(DbContextOptions<appDb> options):base(options)
        {
            
        }

        public DbSet<User> users { get; set; }
        public DbSet<Dashboard> dashboards { get; set; }
        public DbSet<Payment>payments { get; set; }
        public DbSet<website> websites {  get; set; }
        public DbSet<Widget> widgets { get; set; }  



    }
}
