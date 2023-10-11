using Microsoft.EntityFrameworkCore;

namespace GalaxyWave_Dashboard.Api.AccessData
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options){}

       
    }
}
