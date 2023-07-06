using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GalaxyWaves_Media.Areas.User.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions <UserDbContext>options) : base(options)
        {
        }

         
    }
}
