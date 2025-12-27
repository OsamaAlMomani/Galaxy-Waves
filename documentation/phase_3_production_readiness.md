# 🐋 Whale Platform — Phase 3
## Production Readiness & Operational Maturity

---

## Purpose of Phase 3
Phase 3 focuses on **production readiness**, not feature expansion. The goal is to demonstrate that the system is operable, observable, performant, and maintainable under real-world conditions.

By the end of this phase, the system should not only work, but clearly answer:
- How do we know it is healthy?
- How do we debug failures quickly?
- What breaks first under load?
- What is intentionally missing and planned for later phases?

---

## Step 0 — Phase Definition & Scope

### Commands used
```bash
# Rebuild and run the full stack
docker compose up --build -d

# Verify containers
docker compose ps

# Inspect recent API logs
docker logs -n 200 whale-api
```

### Notes
- `--build` is required whenever code changes were made, to avoid running stale Docker images.

---

## Step 1 — System Boundaries & Assumptions

### Commands used
```bash
# Liveness check (API is up)
curl -i http://localhost:8080/health

# Readiness check (API + DB)
curl -i http://localhost:8080/health/ready

# Swagger (development only)
# http://localhost:8080/swagger
```

### System Boundary (Inside the Platform)
- ASP.NET Core API running in Docker
- PostgreSQL database running in Docker
- EF Core for persistence and migrations
- Admin monitoring APIs (Users, Teachers, Courses)
- Admin dashboard aggregation endpoint
- Global JSON error handling
- Development database seeding

### Explicitly Outside the System
- Admin authentication / RBAC
- Rate limiting
- Distributed tracing and metrics
- Background workers or message queues

### Assumptions
- Docker and Docker Compose are installed
- API and DB containers run on the same network
- Admin APIs are used in trusted environments
- Clients respect pagination constraints

---

## Step 2 — Operational Readiness & Observability

### Commands used
```bash
# Bring up stack and follow API logs
docker compose up --build -d
docker logs -f whale-api

# Validate readiness + liveness
curl -i http://localhost:8080/health
curl -i http://localhost:8080/health/ready
```

### Code implemented

#### 1) Health & readiness
**Program.cs**
```csharp
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready");
```

#### 2) Correlation IDs
**Whale.Api/Middleware/CorrelationIdMiddleware.cs**
```csharp
public sealed class CorrelationIdMiddleware
{
    public const string HeaderName = "X-Correlation-Id";
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(HeaderName, out var cid) || StringValues.IsNullOrEmpty(cid))
            cid = Guid.NewGuid().ToString("N");

        context.Items[HeaderName] = cid.ToString();
        context.Response.Headers[HeaderName] = cid.ToString();

        await _next(context);
    }

    public static string? Get(HttpContext context)
        => context.Items.TryGetValue(HeaderName, out var v) ? v?.ToString() : null;
}
```

**Program.cs**
```csharp
app.UseMiddleware<CorrelationIdMiddleware>();
```

#### 3) Request logging
**Program.cs**
```csharp
app.Use(async (context, next) =>
{
    var sw = System.Diagnostics.Stopwatch.StartNew();
    await next();
    sw.Stop();

    var cid = CorrelationIdMiddleware.Get(context) ?? "unknown";
    Console.WriteLine($"[{cid}] {context.Request.Method} {context.Request.Path} -> {context.Response.StatusCode} ({sw.ElapsedMilliseconds}ms)");
});
```

---

## Step 3 — Error Handling Model

### Commands used
```bash
# Rebuild + restart to apply Program.cs changes
docker compose up --build -d

# Quick validation: trigger an invalid request (should return 400)
# Example: missing required fields in body
curl -i -X POST http://localhost:8080/api/deals/import -H "Content-Type: application/json" -d "{}"
```

### Code implemented

#### 1) Unified error contract
**Whale.Api/Contracts/ApiError.cs**
```csharp
public sealed class ApiError
{
    public string Error { get; set; } = "UnknownError";
    public string? Message { get; set; }
    public string? CorrelationId { get; set; }
    public List<ApiFieldError>? Details { get; set; }
}

public sealed class ApiFieldError
{
    public string Field { get; set; } = "";
    public string Message { get; set; } = "";
}
```

#### 2) Validation errors (400)
**Program.cs**
```csharp
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

            return new BadRequestObjectResult(new ApiError
            {
                Error = "ValidationFailed",
                CorrelationId = cid,
                Details = details
            });
        };
    });
```

#### 3) Unexpected errors (500)
**Program.cs**
```csharp
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
```

#### 4) NotFound (404) pattern (controller-level)
Use this inside controllers when a single-resource lookup returns null:
```csharp
return NotFound(new ApiError
{
    Error = "NotFound",
    Message = "Course not found",
    CorrelationId = CorrelationIdMiddleware.Get(HttpContext) ?? "unknown"
});
```

---

## Step 4 — Performance Readiness

### Commands used
```bash
# Create & apply indexes via EF migrations
# (Run from the folder containing Whale.sln or set correct paths)
dotnet ef migrations add Phase3_AddAdminIndexes -p Whale.Infrastructure -s Whale.Api
dotnet ef database update -p Whale.Infrastructure -s Whale.Api

# Rebuild containers to ensure latest code is running
docker compose up --build -d

# Verify indexes exist in Postgres
docker exec -it whale-db psql -U whale_user -d whale -c "\d \"Users\""
docker exec -it whale-db psql -U whale_user -d whale -c "\d \"TeacherProfiles\""
docker exec -it whale-db psql -U whale_user -d whale -c "\d \"Courses\""
```

### Code implemented

#### Database indexing strategy
**Whale.Infrastructure/Persistence/WhaleDbContext.cs**
```csharp
modelBuilder.Entity<User>(e =>
{
    e.HasIndex(x => x.Email).IsUnique();
    e.HasIndex(x => x.Status);
});

modelBuilder.Entity<TeacherProfile>(e =>
{
    e.HasIndex(x => x.UserId).IsUnique();
    e.HasIndex(x => x.VerificationStatus);
});

modelBuilder.Entity<Course>(e =>
{
    e.HasIndex(x => x.Status);
    e.HasIndex(x => x.TeacherUserId);
});
```

### Load testing baseline (k6)

#### Commands used
```bash
# Run k6 locally (if installed)
k6 run k6/admin_baseline.js

# Or run via Docker (no local install)
docker run --rm -i -e BASE_URL=http://host.docker.internal:8080 grafana/k6 run - < k6/admin_baseline.js
```

#### k6 script used
**k6/admin_baseline.js**
```javascript
import http from "k6/http";
import { check, sleep } from "k6";

export const options = {
  vus: 10,
  duration: "20s",
  thresholds: {
    http_req_failed: ["rate<0.01"],
    http_req_duration: ["p(95)<500"],
  },
};

const BASE = __ENV.BASE_URL || "http://localhost:8080";

export default function () {
  const ready = http.get(`${BASE}/health/ready`);
  check(ready, { "health/ready 200": (r) => r.status === 200 });

  const dash = http.get(`${BASE}/api/admin/dashboard`);
  check(dash, { "dashboard 200": (r) => r.status === 200 });

  const users = http.get(`${BASE}/api/admin/users?page=1&pageSize=20`);
  check(users, { "users 200": (r) => r.status === 200 });

  const teachers = http.get(`${BASE}/api/admin/teachers?page=1&pageSize=20`);
  check(teachers, { "teachers 200": (r) => r.status === 200 });

  const courses = http.get(`${BASE}/api/admin/courses?page=1&pageSize=20`);
  check(courses, { "courses 200": (r) => r.status === 200 });

  sleep(1);
}
```

**Results (Local Docker)**
- Error rate: **0%**
- Average latency: **3.81 ms**
- p95 latency: **7.04 ms**
- Max latency: **8.8 ms**
- Throughput: **~49 requests/sec**

---

## Step 5 — Risk Analysis & Forward Planning

### What Would Break First in Production?
1. Dashboard aggregation queries as dataset size grows
2. Filtered admin queries without caching at high volumes
3. Security exposure due to missing admin authentication

### Known Limitations
- Admin endpoints are not protected by authentication/authorization yet
- No rate limiting or abuse prevention
- Observability is limited to logs and correlation IDs
- Performance results are based on local Docker and seeded data

### Planned Mitigations (Future Phases)
- Phase 4: Authentication + RBAC for admin APIs
- Phase 5: Metrics, tracing, and rate limiting
- Phase 6: CI/CD, environment separation, and scaling strategy

---

## Phase 3 Status

✅ **Phase 3 Completed**

The system is now:
- Operationally observable
- Predictable under failure
- Performance-baselined
- Honest about limitations

This concludes Phase 3 and prepares the platform for security hardening and production deployment work in subsequent phases.

