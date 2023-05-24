using Main.Models.AdminModels;
using Main.Models.DepartmentModels;
using Main.Models.TeacherModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Main.Models.WebAccessModel;

namespace Main.DataAccess
{
    public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options){ }

        public DbSet<Category> categories { get; set; }

        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<MusicEquipment> musicEquipment { get; set; }
        public DbSet<Specialization> specialization { get; set; }
        public DbSet<Core>cores { get; set; }
        public DbSet<Course> course { get; set; }


    }
}
