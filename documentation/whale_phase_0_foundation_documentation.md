# 🐋 Whale Platform — Phase 0 Documentation

## Phase 0 — Foundation (Project Initialization)

### 🎯 Goal
Establish a stable, reproducible foundation for the Whale platform without overengineering. Phase 0 ensures that any developer can clone the repository and get the system building consistently.

---

## 0.1 Repository Structure (Final Recommended Layout)

```
whale-platform/
├─ backend/
│  ├─ Whale.sln
│  ├─ Whale.Api/
│  ├─ Whale.Application/
│  ├─ Whale.Domain/
│  └─ Whale.Infrastructure/
│
├─ frontend/
│  └─ admin/
│
├─ documentation/
├─ docker-compose.yml
└─ README.md
```

### Common mistakes
- Keeping multiple frontend templates at the same time
- Mixing backend and frontend files in the same folder

### Recommendation
Keep **one** admin frontend only. Archive or delete unused templates.

---

## 0.2 Backend Solution Initialization

### Commands executed

```bash
dotnet new sln -n Whale

dotnet new webapi -n Whale.Api
dotnet sln add Whale.Api/Whale.Api.csproj

dotnet new classlib -n Whale.Domain
dotnet new classlib -n Whale.Application
dotnet new classlib -n Whale.Infrastructure

dotnet sln add Whale.Domain/Whale.Domain.csproj
dotnet sln add Whale.Application/Whale.Application.csproj
dotnet sln add Whale.Infrastructure/Whale.Infrastructure.csproj

dotnet add Whale.Api reference Whale.Application
dotnet add Whale.Application reference Whale.Domain
dotnet add Whale.Infrastructure reference Whale.Application
```

### Verification

```bash
dotnet restore
dotnet build
```

Build must succeed before moving forward.

---

## 0.3 VS Code Diagnostic Errors (Important Lesson)

### Symptoms
VS Code showed errors such as:
- `Microsoft.EntityFrameworkCore does not exist`
- `AddSwaggerGen not found`
- `Whale.Infrastructure not found`

### Root cause
- VS Code language server cache out of sync
- Opening the wrong folder (not the one containing `Whale.sln`)

### Correct fix
1. Open VS Code on the folder containing `Whale.sln`
2. Restart language server
   - `Ctrl + Shift + P` → `C#: Restart Language Server`
3. Optional cleanup

```powershell
Get-ChildItem -Directory -Recurse -Force -Include bin,obj | Remove-Item -Recurse -Force
```

### Rule
If `dotnet build` is green, the project is healthy. Editor diagnostics are secondary.

---

## Phase 0 Status

✅ Completed successfully

- Clean project structure
- Backend solution initialized
- Build passes consistently
- Tooling baseline established

