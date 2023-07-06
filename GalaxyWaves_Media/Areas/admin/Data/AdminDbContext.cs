using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GalaxyWaves_Media.Areas.admin.Data
{
    public class AdminDbContext:IdentityDbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext>options):base(options) { }

    }
}
