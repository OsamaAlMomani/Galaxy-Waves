# 🐋 Whale Platform — Phase 4
## Security & Access Control (JWT + RBAC)

> **Objective:** Protect operational/admin endpoints (especially `/api/admin/*`) with **JWT authentication** and **role-based access control (RBAC)**, while keeping the implementation clean, testable, and interview-friendly.

---

## Step 0 — Phase 4 Scope Lock (What we will / won’t do)

### Goals (what “done” means)
By the end of Phase 4:

- ✅ A client can **log in** and receive a **JWT**.
- ✅ `/api/admin/*` endpoints require a valid token (**401** if missing/invalid).
- ✅ Only **Admin** role can access admin endpoints (**403** if authenticated but not Admin).
- ✅ Secrets are **not hardcoded** (JWT key/issuer/audience via env/appsettings).
- ✅ Minimum integration tests exist for **401/403/200** behavior.
- ✅ Documentation includes commands + example requests.

### In scope
- JWT authentication (`JwtBearer`)
- RBAC authorization using existing `UserRole` (`Admin | Teacher | Student`)
- Minimal auth endpoint: `POST /api/auth/login`
- Password hashing (recommended): store `PasswordHash` and verify with BCrypt
- Secure defaults for admin controllers (`[Authorize(Roles = "Admin")]`)
- Documentation + commands + test cases

### Out of scope (future phases)
- Refresh tokens / rotation / device sessions
- SSO/OAuth providers (Google/Microsoft)
- Rate limiting / abuse prevention (Phase 5)
- Full observability stack (metrics + tracing) improvements (Phase 5)
- WAF / reverse proxy hardening

### Security rules (baseline)
- `/api/admin/*` → **Admin only**
- `/health` → public (ok for dev; can be internal-only later)
- `/health/ready` → recommend internal-only later; ok public for dev

---

## Phase 4 Implementation Plan (high-level steps)

### Step 1 — Add JWT config + dependencies
- Add `Microsoft.AspNetCore.Authentication.JwtBearer`
- Configure `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`, token lifetime

### Step 2 — Implement token issuance (Auth service)
- Create a small JWT generator service
- Include claims: `sub`, `email`, `role`

### Step 3 — Implement `POST /api/auth/login`
- Validate credentials
- Return `{ token, expiresAt, role }`

### Step 4 — Wire auth into `Program.cs`
- `AddAuthentication().AddJwtBearer(...)`
- `AddAuthorization(...)`
- `app.UseAuthentication();`
- `app.UseAuthorization();`

### Step 5 — Protect Admin APIs with RBAC
- Add `[Authorize(Roles = "Admin")]` on admin controllers

### Step 6 — Add minimum tests (integration)
- `/api/admin/users`:
  - No token → **401**
  - Teacher token → **403**
  - Admin token → **200**

### Step 7 — Document usage (commands + curl examples)
- Docker run with env vars for JWT config
- Example login and authorized requests

---

## Phase 4 Exit Criteria (checklist)
- [ ] Login issues JWT successfully
- [ ] Admin endpoints blocked without token (401)
- [ ] Admin endpoints blocked for non-admin token (403)
- [ ] Admin endpoints allowed for admin token (200)
- [ ] JWT config in env/appsettings, not hardcoded
- [ ] Integration tests added (min 3)
- [ ] Documentation updated with commands + examples

