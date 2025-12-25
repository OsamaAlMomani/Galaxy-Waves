# 🐋 Whale Platform — Phase 1 Documentation

## Phase 1 — Backend Core, Database & Admin APIs

### 🎯 Goal
Transform the project from a static backend into a running system with:
- PostgreSQL
- EF Core
- Dockerized API
- Applied migrations
- Admin monitoring endpoints

---

## 1.1 Required NuGet Packages

### Infrastructure

```bash
dotnet add Whale.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add Whale.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add Whale.Infrastructure package Microsoft.EntityFrameworkCore.Design
```

### API

```bash
dotnet add Whale.Api package Microsoft.EntityFrameworkCore
dotnet add Whale.Api package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add Whale.Api package Swashbuckle.AspNetCore
```

### Required project references

```bash
dotnet add Whale.Api reference Whale.Infrastructure
dotnet add Whale.Infrastructure reference Whale.Domain
```

---

## 1.2 DbContext Setup

**File:** `Whale.Infrastructure/Persistence/WhaleDbContext.cs`

```csharp
using Microsoft.EntityFrameworkCore;

namespace Whale.Infrastructure.Persistence;

public class WhaleDbContext : DbContext
{
    public WhaleDbContext(DbContextOptions<WhaleDbContext> options) : base(options) { }
}
```

---

## 1.3 API Startup Configuration

**File:** `Whale.Api/Program.cs`

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WhaleDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
```

---

## 1.4 Configuration Files

**appsettings.json**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=whale;Username=whale_user;Password=whale_pass"
  }
}
```

### Common error
Invalid JSON syntax caused container crashes.

### Fix
Ensure valid JSON. No trailing commas. No extra quotes.

---

## 1.5 Docker Setup

### Backend Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore Whale.sln
RUN dotnet publish Whale.Api/Whale.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Whale.Api.dll"]
```

### docker-compose.yml

```yaml
services:
  db:
    image: postgres:16
    environment:
      POSTGRES_DB: whale
      POSTGRES_USER: whale_user
      POSTGRES_PASSWORD: whale_pass
    ports:
      - "5432:5432"

  api:
    build:
      context: ./backend
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ConnectionStrings__DefaultConnection: Host=db;Port=5432;Database=whale;Username=whale_user;Password=whale_pass
    ports:
      - "8080:8080"
    depends_on:
      - db
```

---

## 1.6 EF Core Migrations

### Tooling rule
EF Core CLI version must match .NET version.

### Correct version for .NET 9

```bash
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef --version 9.0.11
```

### Migration commands

```bash
dotnet ef migrations add InitialPhase1 --project Whale.Infrastructure --startup-project Whale.Api
dotnet ef database update --project Whale.Infrastructure --startup-project Whale.Api
```

---

## 1.7 Database Verification

```bash
docker exec -it whale-db psql -U whale_user -d whale -c '\dt'
```

Expected tables:
- Users
- TeacherProfiles
- Courses
- __EFMigrationsHistory

---

## 1.8 Admin Endpoints

- `GET /api/admin/users`
- `GET /api/admin/teachers`
- `GET /api/admin/courses`

Swagger available at:
- `http://localhost:8080/swagger`

---

## Phase 1 Status

✅ Completed successfully

- Database connected
- Migrations applied
- Docker containers running
- Admin APIs available

