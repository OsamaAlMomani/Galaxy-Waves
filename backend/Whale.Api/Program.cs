using Microsoft.EntityFrameworkCore;
using Whale.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

// Controllers (real APIs)
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database (PostgreSQL)
builder.Services.AddDbContext<WhaleDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// --------------------
// Middleware~
// --------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection (keep)
app.UseHttpsRedirection();

// Authorization (will matter later)
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
