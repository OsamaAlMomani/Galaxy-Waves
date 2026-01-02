# Frontend-Backend Integration Guide

## Overview
This guide documents the steps to connect the Angular admin frontend to the .NET backend API.

**Backend API:** http://localhost:8080
**Frontend App:** http://localhost:4200

---

## Step 1: Environment Configuration

### 1.1 Update Frontend Environment Files

**File:** `frontend/admin/src/environments/environment.ts`
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:8080/api'
};
```

**File:** `frontend/admin/src/environments/environment.prod.ts`
```typescript
export const environment = {
  production: true,
  apiUrl: 'http://localhost:8080/api'  // Update for production deployment
};
```

**Why:** Centralizes API URL configuration for easy environment switching.

---

## Step 2: CORS Configuration (Backend)

### 2.1 Add CORS Policy to .NET API

**File:** `backend/Whale.Api/Program.cs`

Add before `var app = builder.Build();`:
```csharp
// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
```

Add after `app.UseMiddleware<CorrelationIdMiddleware>();`:
```csharp
// Enable CORS
app.UseCors("AllowAngularApp");
```

**Why:** Allows Angular app (localhost:4200) to make requests to the API (localhost:8080).

---

## Step 3: Update Authentication Service (Frontend)

### 3.1 Modify Authentication Service

**File:** `frontend/admin/src/app/shared/services/authentication.service.ts`

**Before:**
```typescript
const USER_AUTH_API_URL = '/api-url';
```

**After:**
```typescript
import { environment } from '../../../environments/environment';

const USER_AUTH_API_URL = `${environment.apiUrl}/auth/login`;
```

**Update login method to match backend contract:**
```typescript
login(email: string, password: string) {
    return this.http.post<any>(USER_AUTH_API_URL, { email, password })
    .pipe(map(response => {
        if (response && response.token) {
            const user = {
                email: email,
                token: response.token,
                role: response.role || 'User'
            };
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.currentUserSubject.next(user);
        }
        return response;
    }));
}
```

**Why:** 
- Uses centralized API URL from environment config
- Matches backend `/api/auth/login` endpoint
- Stores JWT token for authenticated requests

---

## Step 4: Create HTTP Interceptor for JWT

### 4.1 Create JWT Interceptor

**File:** `frontend/admin/src/app/shared/interceptors/jwt.interceptor.ts` (New file)

```typescript
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const currentUser = JSON.parse(localStorage.getItem('currentUser') || 'null');
        
        if (currentUser && currentUser.token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.token}`
                }
            });
        }

        return next.handle(request);
    }
}
```

**Why:** Automatically adds JWT token to all HTTP requests for authentication.

### 4.2 Create Error Interceptor

**File:** `frontend/admin/src/app/shared/interceptors/error.interceptor.ts` (New file)

```typescript
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                // Auto logout on 401 response
                localStorage.removeItem('currentUser');
                this.router.navigate(['/authentication/login-1']);
            }

            const error = err.error?.message || err.statusText;
            return throwError(() => error);
        }));
    }
}
```

**Why:** Handles authentication errors globally, auto-logout on 401.

### 4.3 Register Interceptors

**File:** `frontend/admin/src/app/app.module.ts`

Add to imports:
```typescript
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/interceptors/jwt.interceptor';
import { ErrorInterceptor } from './shared/interceptors/error.interceptor';
```

Add to providers array:
```typescript
{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
```

**Why:** Registers interceptors globally for all HTTP requests.

---

## Step 5: Update Login Component

### 5.1 Modify Login Component to Use Real API

**File:** `frontend/admin/src/app/authentication/login-1/login-1.component.ts`

Update `submitForm()` method:
```typescript
submitForm(): void {
    if (this.validateForm.valid) {
        this.isLoading = true;
        this.error = false;
        
        const email = this.validateForm.value.email;
        const password = this.validateForm.value.password;
        
        this.authService.login(email, password).subscribe({
            next: (response) => {
                this.isLoading = false;
                this.router.navigate(['/dashboard/demo-one']);
            },
            error: (error) => {
                this.isLoading = false;
                this.error = true;
                console.error('Login failed:', error);
            }
        });
    } else {
        Object.values(this.validateForm.controls).forEach((control) => {
            if (control.invalid) {
                control.markAsDirty();
                control.updateValueAndValidity({ onlySelf: true });
            }
        });
    }
}
```

**Why:** Makes actual API calls instead of mock navigation.

---

## Step 6: Backend API Endpoints Reference

### Available Endpoints

#### Authentication
- `POST /api/auth/login` - Login with email/password
  - Request: `{ "email": "string", "password": "string" }`
  - Response: `{ "token": "string", "email": "string", "role": "string" }`

- `POST /api/auth/register` - Register new user
  - Request: `{ "email": "string", "password": "string", "role": "User|Admin|Teacher|Student" }`

#### Admin Endpoints (Require `[Authorize(Roles = "Admin")]`)
- `GET /api/admin/dashboard` - Dashboard statistics
- `GET /api/admin/users` - List all users
- `GET /api/admin/teachers` - List all teachers
- `GET /api/admin/courses` - List all courses

#### Health Checks
- `GET /health` - Health check
- `GET /health/ready` - Readiness check (API + DB)

---

## Step 7: Testing the Integration

### 7.1 Start Backend
```bash
cd backend
docker-compose up -d
# OR
dotnet run --project Whale.Api/Whale.Api.csproj
```

Verify: http://localhost:8080/health

### 7.2 Start Frontend
```bash
cd frontend/admin
npm install
npm start
```

Access: http://localhost:4200

### 7.3 Test Login Flow
1. Navigate to http://localhost:4200
2. Redirects to login page
3. Enter credentials (use seeded data or register)
4. Should authenticate and redirect to dashboard
5. Check browser DevTools Network tab for API calls

---

## Step 8: Install Frontend Dependencies

```bash
cd frontend/admin
npm install
```

**Required Node Version:** 20.12.0

---

## Troubleshooting

### CORS Issues
**Symptom:** Browser console shows CORS errors
**Solution:** Verify CORS policy in backend `Program.cs` includes `http://localhost:4200`

### 401 Unauthorized
**Symptom:** All API requests return 401
**Solution:** 
- Check JWT token in localStorage
- Verify interceptor is registered
- Confirm backend accepts Bearer token

### Connection Refused
**Symptom:** Cannot connect to localhost:8080
**Solution:**
- Verify backend is running: `docker ps`
- Check backend logs: `docker logs whale-api`
- Ensure port 8080 is not blocked

### Module Not Found
**Symptom:** Angular build errors
**Solution:** Run `npm install` in `frontend/admin`

---

## Security Notes

1. **JWT Storage:** Currently using `localStorage`. Consider `httpOnly` cookies for production.
2. **Password Security:** Backend uses hashed passwords (bcrypt/PBKDF2).
3. **CORS:** Restrict origins in production - remove `localhost:4200`.
4. **HTTPS:** Use HTTPS in production for both frontend and backend.
5. **Environment Variables:** Never commit production API keys/secrets.

---

## Next Steps

1. ✅ Configure environment files
2. ✅ Add CORS to backend
3. ✅ Update authentication service
4. ✅ Create HTTP interceptors
5. ✅ Update login component
6. ⏳ Test end-to-end authentication
7. ⏳ Create additional API services (users, courses, etc.)
8. ⏳ Implement route guards for protected pages
9. ⏳ Add loading states and error handling UI

---

## File Changes Summary

### Backend
- `backend/Whale.Api/Program.cs` - Add CORS configuration

### Frontend
- `frontend/admin/src/environments/environment.ts` - Add API URL
- `frontend/admin/src/environments/environment.prod.ts` - Add API URL
- `frontend/admin/src/app/shared/services/authentication.service.ts` - Update to use real API
- `frontend/admin/src/app/shared/interceptors/jwt.interceptor.ts` - NEW: JWT handler
- `frontend/admin/src/app/shared/interceptors/error.interceptor.ts` - NEW: Error handler
- `frontend/admin/src/app/app.module.ts` - Register interceptors
- `frontend/admin/src/app/authentication/login-1/login-1.component.ts` - Connect to auth service

---

**Last Updated:** January 2, 2026
