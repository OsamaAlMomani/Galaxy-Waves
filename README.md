# Whale Platform

Monorepo layout:

- `backend/` — .NET solution and projects
- `frontend/admin/` — HexaDash Angular (selected build)
- `documentation/` — vendor docs + notes

## Backend

- Solution: `backend/Whale.sln`
- API project: `backend/Whale.Api/Whale.Api.csproj`

Run API (from `whale-platform/`):

- `dotnet run --project backend/Whale.Api/Whale.Api.csproj`

## Frontend (Admin)

The Angular admin UI lives in `frontend/admin/`.

Typical dev commands (from `frontend/admin/`):

- `npm install`
- `npm start` (or `ng serve` depending on scripts)
