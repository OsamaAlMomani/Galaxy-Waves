# Whale Platform — Project Map (Code + Structure)

This repository is a **monorepo** containing:
- A **.NET 9** backend API (PostgreSQL + Entity Framework Core)
- An **Angular Admin** frontend (HexaDash-based)
- Local dev infrastructure via **Docker Compose**
- Internal documentation and performance testing scripts

---

## 1) Repository Layout

Top-level folders/files:

- `docker-compose.yml`
  - Starts **PostgreSQL** (`db`) and the **API** (`api`) for local development.

- `backend/`
  - .NET solution and projects (API + Application + Domain + Infrastructure).

- `frontend/admin/`
  - Angular Admin UI application.

- `documentation/`
  - Project documentation, execution notes, and vendor docs.

- `k6/`
  - Load/performance test scripts.

- `indexes/`
  - Helper indexes / command logs (internal project artifacts).

---

## 2) Backend Overview (.NET)

**Solution**: `backend/Whale.sln`

The backend follows a “layered/clean architecture” style:

- `Whale.Api` — HTTP layer (controllers, middleware, app startup)
- `Whale.Application` — DTOs and application-level models (admin DTOs, paging)
- `Whale.Domain` — business entities/enums (User, TeacherProfile, Course)
- `Whale.Infrastructure` — infrastructure implementations (EF Core DbContext, seeding)

### 2.1 Whale.Api (Web API)

**Path**: `backend/Whale.Api/`

Key files/folders:

- `Program.cs`
  - Registers MVC controllers
  - Adds Swagger (dev)
  - Adds Health Checks (including PostgreSQL check)
  - Configures EF Core `WhaleDbContext` with Npgsql provider
  - Sets up global exception handling (returns a consistent JSON error shape)
  - Adds `CorrelationIdMiddleware` (request/response header: `X-Correlation-Id`)
  - Simple request timing log to console: `[{cid}] METHOD PATH -> STATUS (ms)`
  - Seeds the DB on startup via `DbSeeder.SeedAsync(...)`

- `Middleware/CorrelationIdMiddleware.cs`
  - Ensures every request has a correlation ID (incoming or generated)
  - Echoes the correlation ID in the response header

- `Contracts/ApiError.cs`
  - Standard API error payload used for:
    - Global 500 errors (`UnexpectedError`)
    - Model validation failures (`ValidationFailed` with per-field details)

- `Properties/Controllers/`
  - Admin controllers (query + paging + filtering)

#### Admin endpoints

All current controllers are **read-only list / dashboard** style endpoints:

- `GET /api/admin/dashboard`
  - Returns counts for users/teachers/courses (including statuses).

- `GET /api/admin/users?page=&pageSize=&search=&status=`
  - Paged user listing
  - `search` matches `Email` or `FullName`
  - `status` matches the enum string (e.g., `Active`, `Suspended`)

- `GET /api/admin/teachers?page=&pageSize=&search=&status=`
  - Paged teacher listing
  - `search` matches teacher’s user `Email` or `FullName`
  - `status` matches `TeacherVerificationStatus` (`Pending`, `Approved`, `Rejected`)

- `GET /api/admin/courses?page=&pageSize=&search=&status=`
  - Paged course listing
  - `search` matches `Title`
  - `status` matches `CourseStatus` (`Draft`, `Published`, `Archived`)

#### Health checks

- `GET /health` — liveness
- `GET /health/ready` — readiness (API + DB)

> Note: Health checks include an Npgsql probe wired to `DefaultConnection`.

### 2.2 Whale.Domain (Entities)

**Path**: `backend/Whale.Domain/Entities/`

Main entities:

- `User`
  - `UserRole`: `Admin | Teacher | Student`
  - `UserStatus`: `Active | Inactive | Suspended | Left`

- `TeacherProfile`
  - Links to a `User` by `UserId`
  - `TeacherVerificationStatus`: `Pending | Approved | Rejected`

- `Course`
  - Links to a teacher `User` by `TeacherUserId`
  - `CourseStatus`: `Draft | Published | Archived`
  - `Price` is nullable (`null` means free)

### 2.3 Whale.Application (DTOs and shared models)

**Path**: `backend/Whale.Application/`

Key parts:

- `common/PagedResult.cs`
  - Standard paging container returned by list endpoints:
    - `Page`, `PageSize`, `TotalCount`, `Items`

- `admin/Dto/*`
  - Admin DTOs for API responses (Users/Teachers/Courses/Dashboard)

- `admin/Dto/Queries/AdminListQuery.cs`
  - Shared query model used by list endpoints:
    - `Page`, `PageSize`, `Search`, `Status`

### 2.4 Whale.Infrastructure (Persistence)

**Path**: `backend/Whale.Infrastructure/Persistence/`

- `WhaleDbContext.cs`
  - EF Core DbSets:
    - `Users`, `TeacherProfiles`, `Courses`
  - Model configuration:
    - Unique index on `User.Email`
    - Relationships:
      - `TeacherProfile.UserId` (unique per user)
      - `Course.TeacherUserId` (restrict delete)
    - Additional indexes added for admin filtering/counting

- `DbSeeder.cs`
  - Applies migrations on startup (with retry to handle Docker Compose boot order)
  - Seeds initial data if DB is empty:
    - `admin@whale.local`
    - `teach1@whale.local`, `teach2@whale.local`
    - `stud1@whale.local`, `stud2@whale.local`
    - A couple of sample courses

---

## 3) Configuration

### 3.1 appsettings

- `backend/Whale.Api/appsettings.json`
  - Includes `ConnectionStrings:DefaultConnection` (defaults to localhost Postgres)

### 3.2 Docker Compose overrides

`docker-compose.yml` runs:

- `db` (Postgres 16)
  - Exposes `5432:5432`

- `api` (ASP.NET)
  - Exposes `8080:8080`
  - Sets `ConnectionStrings__DefaultConnection` to point to `Host=db` (the compose network)

---

## 4) How to Run

### Option A — Run everything with Docker

From repo root:

```bash
docker compose up --build
```

- API: http://localhost:8080
- Postgres: localhost:5432

If `ASPNETCORE_ENVIRONMENT=Development`, Swagger UI is enabled.

### Option B — Run backend locally (without Docker)

Prereqs:
- .NET SDK 9.x
- A local Postgres database reachable by `DefaultConnection`

From repo root:

```bash
dotnet run --project backend/Whale.Api/Whale.Api.csproj
```

Or watch mode:

```bash
dotnet watch run --project backend/Whale.Api/Whale.Api.csproj
```

> On startup, the API runs migrations and seeds data automatically.

### Option C — Run frontend admin

Prereqs:
- Node.js (a recent LTS is recommended)

From `frontend/admin/`:

```bash
npm install
npm start
```

Default dev server: http://localhost:4200

---

## 5) Notes / Known Artifacts

- `backend/Whale.Api/Whale.Api.http` currently references `/weatherforecast` and port `5227`.
  - This looks like a template file and may not match the current API routes/ports.

- `k6/admin_baseline.js`
  - Contains baseline load-test scripting for admin endpoints.

---

## 6) Developer Workflow Quick Commands

### Backend

- Build solution:

```bash
dotnet build backend/Whale.sln
```

- Publish API:

```bash
dotnet publish backend/Whale.Api/Whale.Api.csproj
```

### Frontend

- Dev server:

```bash
npm start
```

- Production build:

```bash
npm run build-prod
```
