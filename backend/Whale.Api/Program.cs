using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Whale.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddDbContext<WhaleDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// --------------------
// Global error handling (should be early)
// --------------------
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            error = "Unexpected server error"
        });
    });
});

// --------------------
// Development tools
// --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --------------------
// Middleware pipeline
// --------------------
app.UseHttpsRedirection();

app.UseAuthorization();

// --------------------
// Endpoints
// --------------------
app.MapHealthChecks("/health");
app.MapControllers();

// --------------------
// Database seed (after DI + before app.Run)
// --------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WhaleDbContext>();
    await DbSeeder.SeedAsync(db);
}

app.Run();
