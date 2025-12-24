using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using System.Linq;

namespace ProductService.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<ProductService.Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProductDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<ProductDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Replace authentication with a test scheme that auto-authenticates
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                })
                .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
                    db.Database.EnsureCreated();
                }
            });

            base.ConfigureWebHost(builder);
        }
    }
}
