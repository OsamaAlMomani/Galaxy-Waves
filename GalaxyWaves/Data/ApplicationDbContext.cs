using Galaxy.Core.Models.AdminSite;
using Galaxy.Core.ViewModelComponent;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GalaxyWaves.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public DbSet<TeacherProfile> Teacher_Profile { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Equipment> equipment { get; set; }
        public DbSet<LoginViewModel> LoginViewModel { get; set; }
        public DbSet<RegisterViewModel> RegisterViewModel { get; set; }

    }
}
