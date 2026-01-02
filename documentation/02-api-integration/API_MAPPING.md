# API Mapping Documentation

This document maps the backend .NET API endpoints to the frontend Angular services.

## Backend API Base URL
- **Development:** `http://localhost:8080/api`
- **Production:** Configure in `environment.prod.ts`

---

## Authentication APIs

### Login
- **Backend:** `POST /api/auth/login`
- **Frontend Service:** `AuthenticationService.login(email, password)`
- **File:** `src/app/shared/services/authentication.service.ts`
- **Request:**
  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```
- **Response:**
  ```json
  {
    "token": "string",
    "email": "string",
    "role": "string"
  }
  ```
- **Authorization:** None (public endpoint)

### Register
- **Backend:** `POST /api/auth/register`
- **Frontend Service:** Not yet implemented
- **Request:**
  ```json
  {
    "email": "string",
    "password": "string",
    "role": "User|Admin|Teacher|Student"
  }
  ```
- **Authorization:** None (public endpoint)

---

## Admin Dashboard APIs

### Get Dashboard Statistics
- **Backend:** `GET /api/admin/dashboard`
- **Frontend Service:** `AdminDashboardService.getDashboard()`
- **File:** `src/app/shared/services/admin/admin-dashboard.service.ts`
- **Response:**
  ```typescript
  {
    totalUsers: number;
    activeUsers: number;
    totalTeachers: number;
    pendingTeachers: number;
    approvedTeachers: number;
    totalCourses: number;
    publishedCourses: number;
    draftCourses: number;
  }
  ```
- **Authorization:** Admin role required
- **Usage Example:**
  ```typescript
  constructor(private dashboardService: AdminDashboardService) {}
  
  loadDashboard() {
    this.dashboardService.getDashboard().subscribe({
      next: (data) => console.log(data),
      error: (err) => console.error(err)
    });
  }
  ```

---

## Admin Users APIs

### Get Users List (Paginated)
- **Backend:** `GET /api/admin/users`
- **Frontend Service:** `AdminUsersService.getUsers(query?)`
- **File:** `src/app/shared/services/admin/admin-users.service.ts`
- **Query Parameters:**
  - `page` (optional, default: 1)
  - `pageSize` (optional, default: 20, max: 100)
  - `search` (optional) - Searches email and full name
  - `status` (optional) - Filters by user status (Active, Inactive, etc.)
- **Response:**
  ```typescript
  {
    items: UserListItemDto[];
    total: number;
    page: number;
    pageSize: number;
    totalPages: number;
  }
  ```
- **Authorization:** Admin role required
- **Usage Example:**
  ```typescript
  constructor(private usersService: AdminUsersService) {}
  
  loadUsers() {
    const query = {
      page: 1,
      pageSize: 20,
      search: 'john',
      status: 'Active'
    };
    
    this.usersService.getUsers(query).subscribe({
      next: (result) => {
        this.users = result.items;
        this.totalUsers = result.total;
      },
      error: (err) => console.error(err)
    });
  }
  ```

---

## Admin Teachers APIs

### Get Teachers List (Paginated)
- **Backend:** `GET /api/admin/teachers`
- **Frontend Service:** `AdminTeachersService.getTeachers(query?)`
- **File:** `src/app/shared/services/admin/admin-teachers.service.ts`
- **Query Parameters:**
  - `page` (optional, default: 1)
  - `pageSize` (optional, default: 20, max: 100)
  - `search` (optional) - Searches email and full name
  - `status` (optional) - Filters by verification status (Pending, Approved, Rejected)
- **Response:**
  ```typescript
  {
    items: TeacherListItemDto[];
    total: number;
    page: number;
    pageSize: number;
    totalPages: number;
  }
  ```
- **Authorization:** Admin role required
- **Usage Example:**
  ```typescript
  constructor(private teachersService: AdminTeachersService) {}
  
  loadTeachers() {
    const query = {
      page: 1,
      pageSize: 20,
      status: 'Pending'
    };
    
    this.teachersService.getTeachers(query).subscribe({
      next: (result) => {
        this.teachers = result.items;
        this.totalTeachers = result.total;
      },
      error: (err) => console.error(err)
    });
  }
  ```

---

## Admin Courses APIs

### Get Courses List (Paginated)
- **Backend:** `GET /api/admin/courses`
- **Frontend Service:** `AdminCoursesService.getCourses(query?)`
- **File:** `src/app/shared/services/admin/admin-courses.service.ts`
- **Query Parameters:**
  - `page` (optional, default: 1)
  - `pageSize` (optional, default: 20, max: 100)
  - `search` (optional) - Searches course title
  - `status` (optional) - Filters by course status (Draft, Published, Archived)
- **Response:**
  ```typescript
  {
    items: CourseListItemDto[];
    total: number;
    page: number;
    pageSize: number;
    totalPages: number;
  }
  ```
- **Authorization:** Admin role required
- **Usage Example:**
  ```typescript
  constructor(private coursesService: AdminCoursesService) {}
  
  loadCourses() {
    const query = {
      page: 1,
      pageSize: 20,
      search: 'angular',
      status: 'Published'
    };
    
    this.coursesService.getCourses(query).subscribe({
      next: (result) => {
        this.courses = result.items;
        this.totalCourses = result.total;
      },
      error: (err) => console.error(err)
    });
  }
  ```

---

## Health Check APIs

### Basic Health Check
- **Backend:** `GET /health`
- **Response:** `Healthy`
- **Authorization:** None

### Readiness Check (API + Database)
- **Backend:** `GET /health/ready`
- **Response:** JSON with health status
- **Authorization:** None

---

## File Structure

```
frontend/admin/src/app/shared/
├── models/
│   └── admin.models.ts              # TypeScript interfaces for API responses
├── services/
│   ├── authentication.service.ts    # Auth API (login, logout)
│   └── admin/
│       ├── admin-dashboard.service.ts   # Dashboard stats API
│       ├── admin-users.service.ts       # Users management API
│       ├── admin-teachers.service.ts    # Teachers management API
│       └── admin-courses.service.ts     # Courses management API
└── interceptors/
    ├── jwt.interceptor.ts           # Adds JWT token to requests
    └── error.interceptor.ts         # Handles 401 errors
```

---

## Error Handling

All services use the `ErrorInterceptor` which:
- Automatically catches 401 Unauthorized responses
- Removes user from localStorage
- Redirects to login page

**Custom error handling example:**
```typescript
this.usersService.getUsers().subscribe({
  next: (result) => this.users = result.items,
  error: (err) => {
    // Handle specific errors
    if (err.includes('forbidden')) {
      this.showMessage('You do not have permission');
    } else {
      this.showMessage('An error occurred');
    }
  }
});
```

---

## Authentication Flow

1. User logs in via `AuthenticationService.login(email, password)`
2. Backend returns JWT token
3. Token stored in `localStorage` as `currentUser`
4. `JwtInterceptor` adds token to all subsequent requests as `Authorization: Bearer {token}`
5. Backend validates token on protected routes
6. If token invalid/expired, backend returns 401
7. `ErrorInterceptor` catches 401, clears storage, redirects to login

---

## CORS Configuration

Backend is configured to accept requests from:
- `http://localhost:4200` (Angular dev server)

For production, update `Program.cs` CORS policy with production frontend URL.

---

## Models Reference

All TypeScript models are defined in `src/app/shared/models/admin.models.ts`:

- `AdminDashboardDto` - Dashboard statistics
- `UserListItemDto` - User list item
- `TeacherListItemDto` - Teacher list item
- `CourseListItemDto` - Course list item
- `PagedResult<T>` - Generic pagination wrapper
- `AdminListQuery` - Query parameters for list endpoints

---

**Last Updated:** January 2, 2026
