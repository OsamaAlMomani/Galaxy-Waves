using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GalaxyWaves_UI.Models.WebAccessModel;
using GalaxyWaves_UI.Models.AdminModels;
using GalaxyWaves_UI.Models.DepartmentModels;
using GalaxyWaves_UI.Models.TeacherModels;
using GalaxyWaves_UI.Models.SharedModels;

namespace GalaxyWaves_UI.DataAccess
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Profile>().Property(x => x.Name).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.Twitter).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.Outlook).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.PersonalWebSite).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.CardHolderName).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.Description).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.CardNumber).HasDefaultValue(0);
            builder.Entity<Profile>().Property(x => x.CardDate).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.Country).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.CVC).HasDefaultValue(0);
            builder.Entity<Profile>().Property(x => x.DOB).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.Facebook).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.Gender).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.Gmail).HasDefaultValue("None");
            builder.Entity<Profile>().Property(x => x.LinkedIn).HasDefaultValue("None");
        }

        public DbSet<Category> categories { get; set; }

        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<MusicEquipment> musicEquipment { get; set; }
        public DbSet<Specialization> specialization { get; set; }
        public DbSet<Core> cores { get; set; }
        public DbSet<Course> course { get; set; }

        public DbSet<Profile> profile { get; set; }


    }
}
