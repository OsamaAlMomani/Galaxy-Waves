using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Whale.Api.Auth;
using Whale.Api.Contracts;
using Whale.Api.Middleware;
using Whale.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var cid = CorrelationIdMiddleware.Get(context.HttpContext) ?? "unknown";

            var details = context.ModelState
                .Where(kvp => kvp.Value?.Errors.Count > 0)
                .SelectMany(kvp => kvp.Value!.Errors.Select(e => new ApiFieldError
                {
                    Field = kvp.Key,
                    Message = string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid value" : e.ErrorMessage
                }))
                .ToList();

            var payload = new ApiError
            {
                Error = "ValidationFailed",
                CorrelationId = cid,
                Details = details
            };

            return new BadRequestObjectResult(payload);
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton<JwtTokenService>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddDbContext<WhaleDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// --------------------
// Global error handling (early)
// --------------------
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var cid = CorrelationIdMiddleware.Get(context) ?? "unknown";

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new ApiError
        {
            Error = "UnexpectedError",
            Message = "Unexpected server error",
            CorrelationId = cid
        });
    });
});

// --------------------
// Correlation ID (must be early)
// --------------------
app.UseMiddleware<CorrelationIdMiddleware>();

// --------------------
// Request logging (after correlation id)
// --------------------
app.Use(async (context, next) =>
{
    var sw = System.Diagnostics.Stopwatch.StartNew();
    await next();
    sw.Stop();

    var cid = CorrelationIdMiddleware.Get(context) ?? "unknown";
    var method = context.Request.Method;
    var path = context.Request.Path.Value ?? "/";
    var status = context.Response.StatusCode;

    Console.WriteLine($"[{cid}] {method} {path} -> {status} ({sw.ElapsedMilliseconds}ms)");
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
// NOTE: In Docker, HTTPS redirection can cause confusion if TLS is terminated elsewhere.
// Keep it if you're sure about your reverse proxy setup.
// app.UseHttpsRedirection();

app.UseAuthorization();

// --------------------
// Endpoints
// --------------------
app.MapHealthChecks("/health");        // liveness (API is up)
app.MapHealthChecks("/health/ready");  // readiness (API + DB)
app.MapControllers();

// --------------------
// Database seed
// --------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WhaleDbContext>();
    await DbSeeder.SeedAsync(db);
}

app.Run();
