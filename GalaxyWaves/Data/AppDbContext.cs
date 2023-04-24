using Galaxy.Core.Models.C_Level.Departments;
using Galaxy.Core.Models.C_Level.Profile;
using Galaxy.Core.Models.C_Level.Tools;
using Galaxy.Core.Models.Guide;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Galaxy.Core.Models.Student_Profile;
using Galaxy.Core.ViewModelComponent;
using GalaxyWaves.Models;

namespace GalaxyWaves.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<CLVLProfile> C_Level { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Equipment> equipment { get; set; }
        public DbSet<GuideEquipment> guideEquipment { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<Guide> guide { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; } = default!;
        public DbSet<LoginViewModel> LoginViewModel { get; set; } = default!;
        public DbSet<RegisterViewModel> RegisterViewModel { get; set; } = default!;
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
