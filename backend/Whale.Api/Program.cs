using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Whale.Api.Auth;
using Whale.Api.Contracts;
using Whale.Api.Middleware;
using Whale.Infrastructure.Persistence;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

var jwt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;
var key = Encoding.UTF8.GetBytes(jwt.Key);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });



builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();


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

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddDbContext<WhaleDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("auth", limiter =>
    {
        limiter.PermitLimit = 10;
        limiter.Window = TimeSpan.FromMinutes(1);
        limiter.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiter.QueueLimit = 0;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

// --------------------
// Global error handling (early)
// --------------------
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var correlationId = context.Items["CorrelationId"]?.ToString();

        if (feature != null)
        {
            logger.LogError(
                feature.Error,
                "Unhandled exception (CorrelationId={CorrelationId})",
                correlationId
            );
        }

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            error = "Internal server error",
            correlationId,
            timestamp = DateTime.UtcNow
        });
    });
});


// --------------------
// Correlation ID (must be early)
// --------------------
app.UseMiddleware<CorrelationIdMiddleware>();

// --------------------
// CORS (must be before authentication/authorization)
// --------------------
app.UseCors("AllowAngularApp");

// --------------------
// Request logging (after correlation id)
// --------------------
app.Use(async (context, next) =>
{
    var sw = System.Diagnostics.Stopwatch.StartNew();
    await next();
    sw.Stop();

    var cid = CorrelationIdMiddleware.Get(context) ?? "unknown";
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("HTTP");

    logger.LogInformation(
        "Request {Method} {Path} responded {StatusCode} in {ElapsedMs}ms (CorrelationId={CorrelationId})",
        context.Request.Method,
        context.Request.Path.Value,
        context.Response.StatusCode,
        sw.ElapsedMilliseconds,
        cid
    );
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

app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    var sw = Stopwatch.StartNew();

    await next();

    sw.Stop();
    logger.LogInformation(
        "Request completed {StatusCode} in {Elapsed}ms",
        context.Response.StatusCode,
        sw.ElapsedMilliseconds
    );
});

// --------------------
// Endpoints
// --------------------
app.MapHealthChecks("/health");        // liveness (API is up)
app.MapHealthChecks("/health/ready");  // readiness (API + DB)


app.Use(async (context, next) =>
{
    var correlationId =
        context.Request.Headers["X-Correlation-Id"].FirstOrDefault()
        ?? Guid.NewGuid().ToString();

    context.Items["CorrelationId"] = correlationId;
    context.Response.Headers["X-Correlation-Id"] = correlationId;

    using (context.RequestServices
        .GetRequiredService<ILoggerFactory>()
        .CreateLogger("RequestScope")
        .BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["Method"] = context.Request.Method,
            ["Path"] = context.Request.Path
        }))
    {
        await next();
    }
});
app.UseRateLimiter();


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
