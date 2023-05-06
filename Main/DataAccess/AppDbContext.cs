using Main.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Main.DataAccess
{
    public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options){ }

        public DbSet<Category> categories { get; set; }

        public DbSet<Teacher> Teacher { get; set; }


    }
}
