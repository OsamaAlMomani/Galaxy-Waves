# Whale Platform — File Structure (Per Folder / Key Files)

This doc prints the **source structure** of the repo and explains what each file/script is for.

Notes:
- `bin/` and `obj/` folders are **build output** and are intentionally **not documented** here.
- Frontend Angular `node_modules/` is also not documented.

---

## 1) Root

```
whale-platform/
  docker-compose.yml
  README.md
  .gitignore
  backend/
  frontend/
  documentation/
  k6/
  indexes/
```

- `docker-compose.yml`
  - Local dev stack: `db` (Postgres 16) + `api` (ASP.NET) on port `8080`.
  - Injects connection string to API via `ConnectionStrings__DefaultConnection`.

- `README.md`
  - Quick overview + pointers to docs.

- `.gitignore`
  - Ignores build outputs (`**/bin`, `**/obj`) and editor artifacts.

---

## 2) Backend (C# / .NET 9)

```
backend/
  Dockerfile
  Whale.sln
  Whale.Api/
  Whale.Application/
  Whale.Domain/
  Whale.Infrastructure/
```

### 2.1 backend/Dockerfile

- Builds and publishes the API into a runtime container.
- Exposes the API on port `8080` inside the container.

### 2.2 backend/Whale.sln

- Visual Studio solution including:
  - `Whale.Api`
  - `Whale.Application`
  - `Whale.Domain`
  - `Whale.Infrastructure`

---

## 3) Whale.Api (HTTP API)

```
backend/Whale.Api/
  Program.cs
  appsettings.json
  appsettings.Development.json
  Whale.Api.csproj
  Whale.Api.http
  Contracts/
    ApiError.cs
  Middleware/
    CorrelationIdMiddleware.cs
  Properties/
    launchSettings.json
    Controllers/
      AdminDashboardController.cs
      AdminUsersController.cs
      AdminTeachersController.cs
      AdminCoursesController.cs
```

- `Program.cs`
  - ASP.NET app startup: controllers + Swagger + health checks + EF Core DbContext.
  - Adds global exception handler to return a consistent JSON `ApiError`.
  - Adds correlation ID middleware (`X-Correlation-Id`) and request timing logs.
  - Applies migrations + seeds DB at startup via `DbSeeder.SeedAsync`.

- `appsettings.json`
  - Default configuration (includes local Postgres `DefaultConnection`).

- `Whale.Api.csproj`
  - Web SDK project targeting `net9.0`.
  - References EF Core, Npgsql provider, Swagger.

- `Whale.Api.http`
  - REST client scratch file (currently template-like; may not match current routes/port).

**Contracts**
- `Contracts/ApiError.cs`
  - Standard error payloads returned on validation errors and unexpected exceptions.

**Middleware**
- `Middleware/CorrelationIdMiddleware.cs`
  - Ensures a correlation id exists for every request.
  - Stores it in `HttpContext.Items` and echoes it back in response headers.

**Controllers (Admin APIs)**
- `AdminDashboardController.cs`
  - `GET /api/admin/dashboard` returns aggregated counts (users/teachers/courses).

- `AdminUsersController.cs`
  - `GET /api/admin/users` returns a paged list of users.
  - Supports `page`, `pageSize`, `search`, `status`.

- `AdminTeachersController.cs`
  - `GET /api/admin/teachers` returns a paged list of teacher profiles + user info.
  - Supports `page`, `pageSize`, `search`, `status`.

- `AdminCoursesController.cs`
  - `GET /api/admin/courses` returns a paged list of courses + teacher user info.
  - Supports `page`, `pageSize`, `search`, `status`.

---

## 4) Whale.Domain (Entities)

```
backend/Whale.Domain/
  Whale.Domain.csproj
  Entities/
    User.cs
    TeacherProfile.cs
    Course.cs
```

- `Entities/User.cs`
  - User model + enums: `UserRole`, `UserStatus`.
  - Includes `PasswordHash` column (used by Phase 4 migration).

- `Entities/TeacherProfile.cs`
  - Teacher profile linked to a `User`.
  - Enum `TeacherVerificationStatus`.

- `Entities/Course.cs`
  - Course linked to a teacher `User`.
  - Enum `CourseStatus`.

---

## 5) Whale.Application (DTOs / Query models)

```
backend/Whale.Application/
  Whale.Application.csproj
  common/
    PagedResult.cs
  admin/
    Dto/
      AdminDashboardDto.cs
      UserListItemDto.cs
      TeacherListItemDto.cs
      CourseListItemDto.cs
      Queries/
        AdminListQuery.cs
```

- `common/PagedResult.cs`
  - Shared paging wrapper returned by list endpoints.

- `admin/Dto/AdminDashboardDto.cs`
  - DTO returned by `/api/admin/dashboard`.

- `admin/Dto/UserListItemDto.cs`
  - DTO for admin user listing rows.

- `admin/Dto/TeacherListItemDto.cs`
  - DTO for admin teacher listing rows.

- `admin/Dto/CourseListItemDto.cs`
  - DTO for admin course listing rows.

- `admin/Dto/Queries/AdminListQuery.cs`
  - Query parameters for list endpoints: `Page`, `PageSize`, `Search`, `Status`.

---

## 6) Whale.Infrastructure (Persistence + Migrations)

```
backend/Whale.Infrastructure/
  Whale.Infrastructure.csproj
  Persistence/
    WhaleDbContext.cs
    DbSeeder.cs
    Migrations/
      *_InitialCreate.cs
      *_PendingModelChanges_2025_12_27.cs
      *_Phase4_AddPasswordHash.cs
      WhaleDbContextModelSnapshot.cs
```

- `Persistence/WhaleDbContext.cs`
  - EF Core DbContext.
  - Defines DbSets: `Users`, `TeacherProfiles`, `Courses`.
  - Defines indexes and relationships.

- `Persistence/DbSeeder.cs`
  - Runs `db.Database.MigrateAsync()` with retries (Docker startup friendly).
  - Seeds initial users/teachers/courses if DB is empty.

- `Persistence/Migrations/*`
  - EF Core migrations history + snapshot.
  - `*_Phase4_AddPasswordHash.cs` adds `Users.PasswordHash`.

---

## 7) Frontend (Angular Admin)

```
frontend/
  admin/
    package.json
    angular.json
    src/
    ...
```

- `frontend/admin/package.json`
  - Angular 17 app scripts: `start`, `build`, `test`, etc.

> The Angular `src/` folder is large; if you want, I can generate a second doc just for `frontend/admin/src/` with a per-feature breakdown.

---

## 8) k6 (Performance scripts)

```
k6/
  admin_baseline.js
```

- `k6/admin_baseline.js`
  - Baseline load test for health checks and admin endpoints.
  - Default target: `http://localhost:8080` (override with `BASE_URL`).
