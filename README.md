# Galaxy-Waves — Monorepo

Purpose: host the frontend UI (`GalaxyWave.UI`) and the API (`GalaxyWave_Dashboard.Api`) as small services and support the `Server_Side.*` libraries used by middleware.

Recommended structure (suggested, not yet enforced):

- src/
	- services/
		- GalaxyWave.UI/                 # frontend web app (ASP.NET Core)
		- GalaxyWave_Dashboard.Api/      # API (ASP.NET Core Web API)
		- Server_Side.MiddleWare/        # middleware / service host
	- libs/
		- Server_Side.Core/              # shared domain libraries
		- Server_Side.Core.Courses/
		- Server_Side.Core.Categories/

What I changed for you:
- Created top-level solution `GalaxyWaves.sln` that includes all projects.
- Removed duplicate/space-containing project filename and fixed project references in middleware.
- Added a `Dockerfile` for the API and a `docker-compose.yml` to run the UI and API together.

How to build locally (dotnet):
```powershell
dotnet restore
dotnet build GalaxyWaves.sln
```

How to run via Docker Compose (builds images first):
```powershell
# from repo root
docker-compose build
docker-compose up --remove-orphans
```

Next recommended steps (I can apply any of these):
- Reorganize files into `src/services` and `src/libs` (move projects into those folders and update the solution).
- Add per-service Dockerfiles where missing and adjust base images to a single TFM (`net8.0`) if you want to upgrade.
- Add a CI workflow that builds the solution and builds/pushes Docker images.
- Add small health-check endpoints and a reverse-proxy (Traefik/Nginx) if you want a single external port.
# Galaxy Waves
