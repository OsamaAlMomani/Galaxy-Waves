using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Whale.Api.Auth;
using Whale.Api.Contracts;
using Whale.Api.Contracts.Auth;
using Whale.Api.Middleware;
using Whale.Infrastructure.Persistence;
using Microsoft.AspNetCore.RateLimiting;

namespace Whale.Api.Controllers;


[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly WhaleDbContext _db;
    private readonly JwtTokenService _jwt;

    public AuthController(WhaleDbContext db, JwtTokenService jwt)
    {
        _db = db;
        _jwt = jwt;
    }
    
    [EnableRateLimiting("auth")]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var email = (request.Email ?? "").Trim().ToLowerInvariant();
        var password = request.Password ?? "";

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest(new ApiError
            {
                Error = "ValidationFailed",
                CorrelationId = CorrelationIdMiddleware.Get(HttpContext) ?? "unknown",
                Details = new List<ApiFieldError>
                {
                    new() { Field = "email", Message = "Email is required" },
                    new() { Field = "password", Message = "Password is required" }
                }
            });
        }

        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.ToLower() == email);
        if (user is null || string.IsNullOrWhiteSpace(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return Unauthorized(new ApiError
            {
                Error = "InvalidCredentials",
                Message = "Invalid email or password",
                CorrelationId = CorrelationIdMiddleware.Get(HttpContext) ?? "unknown"
            });
        }

        var (token, expiresAt) = _jwt.CreateToken(user);
        return Ok(new LoginResponse
        {
            Token = token,
            ExpiresAt = expiresAt,
            Role = user.Role.ToString()
        });
    }
}
