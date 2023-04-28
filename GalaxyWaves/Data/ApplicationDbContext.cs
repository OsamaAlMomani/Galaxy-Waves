using Galaxy.Core.Models.AdminSite;
using Galaxy.Core.ViewModelComponent;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Galaxy.Core.Models.Guide;
using Galaxy.Core.Models;

namespace GalaxyWaves.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<TeacherProfile> Teacher_Profile { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Equipment> equipment { get; set; }
        public DbSet<LoginViewModel> LoginViewModel { get; set; }
        public DbSet<RegisterViewModel> RegisterViewModel { get; set; }
        public DbSet<Course> Course { get; set; } = default!;
        //public DbSet<PaymentMethod> paymentmethod { get; set; }

    }
}
