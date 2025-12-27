using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Whale.Api.Middleware;

public sealed class CorrelationIdMiddleware
{
    public const string HeaderName = "X-Correlation-Id";
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        // Use incoming correlation id if present, otherwise generate one
        if (!context.Request.Headers.TryGetValue(HeaderName, out StringValues cid) || StringValues.IsNullOrEmpty(cid))
        {
            cid = Guid.NewGuid().ToString("N");
        }

        // Store for downstream usage
        context.Items[HeaderName] = cid.ToString();

        // Echo back in response so clients can report it
        context.Response.Headers[HeaderName] = cid.ToString();

        await _next(context);
    }

    public static string? Get(HttpContext context)
        => context.Items.TryGetValue(HeaderName, out var v) ? v?.ToString() : null;
}
