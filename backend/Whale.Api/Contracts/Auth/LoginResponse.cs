namespace Whale.Api.Contracts.Auth;

public sealed class LoginResponse
{
    public string Token { get; set; } = "";
    public DateTimeOffset ExpiresAt { get; set; }
    public string Role { get; set; } = "";
}
