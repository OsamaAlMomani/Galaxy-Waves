using GalaxyWaves_Media.Areas.admin.Data;
using GalaxyWaves_Media.Areas.User.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AdminDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AdminDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope= app.Services.CreateScope() )
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var _RoleManager = new[] { "admin", "manager", "user" };
    foreach(var _role in _RoleManager)
    {
        if (!await roleManager.RoleExistsAsync(_role))
        { await roleManager.CreateAsync(new IdentityRole(_role)); }
    }
}
using (var scope= app.Services.CreateScope() )
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser >>();
    var _Email = "admin@admin.com";
    var _Password = "Admin1324!";

    if (await userManager.FindByEmailAsync(_Email) == null)
    {
        var USER = new IdentityUser();
        USER.UserName = _Email; USER.Email = _Email;

        await userManager.CreateAsync(USER, _Password);
        await userManager.AddToRoleAsync(USER, "admin");
    }
}

app.Run();
