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

        //public DbSet<PaymentMethod> paymentmethod { get; set; }

    }
}
