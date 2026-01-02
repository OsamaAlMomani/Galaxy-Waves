# Quick Test Guide - Admin API Services

## Test Pages Created

### 1. Login Test Page
**URL**: http://localhost:4200/authentication/login-test

**Features**:
- Simple login form with email/password inputs
- Test the backend authentication endpoint
- Display JWT token on successful login
- Show current user from localStorage
- Auto-redirect to admin-test page after login

**Default Credentials**:
```
Email: admin@whale.com
Password: admin123
```

### 2. Admin API Test Page
**URL**: http://localhost:4200/dashboard/admin-test

**Features**:
- **Dashboard Statistics**: Load admin dashboard overview
- **Users Management**: Search users with pagination (page, pageSize, search)
- **Teachers Management**: Search teachers with pagination
- **Courses Management**: Search courses with pagination

**Each Section Includes**:
- Input fields for query parameters
- Load button to fetch data
- Loading indicators
- Error messages display
- JSON response preview

## How to Test

### Step 1: Login
1. Navigate to: http://localhost:4200/authentication/login-test
2. Use credentials: `admin@whale.com` / `admin123`
3. Click "Login" button
4. If successful, you'll see the JWT token
5. After 2 seconds, auto-redirects to admin-test page

### Step 2: Test Dashboard API
1. On the admin-test page, click "Load Dashboard"
2. View the dashboard statistics in the JSON response

### Step 3: Test Users API
1. Scroll to "Users Management" section
2. Optional: Enter search text (e.g., "john")
3. Set page number (default: 1)
4. Set page size (default: 10)
5. Click "Load Users"
6. View paginated results with total count

### Step 4: Test Teachers API
1. Scroll to "Teachers Management" section
2. Enter search parameters if needed
3. Click "Load Teachers"
4. View results

### Step 5: Test Courses API
1. Scroll to "Courses Management" section
2. Enter search parameters if needed
3. Click "Load Courses"
4. View results

## API Endpoints Being Tested

### Authentication
- `POST /api/auth/login` - User login

### Admin Endpoints (Require JWT Token)
- `GET /api/admin/dashboard` - Dashboard statistics
- `GET /api/admin/users?page=1&pageSize=10&search=text` - Users list
- `GET /api/admin/teachers?page=1&pageSize=10&search=text` - Teachers list
- `GET /api/admin/courses?page=1&pageSize=10&search=text` - Courses list

## Troubleshooting

### Token Issues
- If you get 401 errors, click "Logout" and login again
- Check browser console for error details
- Verify token is stored in localStorage (key: 'currentUser')

### CORS Issues
- Backend CORS is configured for http://localhost:4200
- Check browser console for CORS errors
- Verify backend is running: http://localhost:8080/health

### Backend Not Responding
1. Check Docker containers: `docker compose ps`
2. Verify backend health: `curl http://localhost:8080/health`
3. Check backend logs: `docker compose logs api`

## Browser Developer Tools

### Network Tab
- Monitor HTTP requests
- Check request headers (Authorization: Bearer ...)
- View response status codes
- Inspect response bodies

### Console Tab
- View error messages
- Check interceptor logs
- Debug service calls

### Application Tab
- Check localStorage → currentUser
- Verify JWT token is stored
- View token structure

## Expected Behavior

### Successful Login
✅ Status 200
✅ Token received and stored in localStorage
✅ User object: { email, token, role }
✅ Auto-redirect to admin-test page

### Successful API Call
✅ Status 200
✅ Authorization header sent automatically
✅ Data displayed in JSON format
✅ Total count and pagination info shown

### Failed Requests
❌ Status 401 → Auto logout, redirect to login
❌ Status 400/500 → Error message displayed
❌ Network error → Error message displayed

## Services Architecture

All test pages use these Angular services:

1. **AuthenticationService** (`shared/services/authentication.service.ts`)
   - login(email, password)
   - logout()
   - currentUserValue

2. **AdminDashboardService** (`shared/services/admin/admin-dashboard.service.ts`)
   - getDashboard()

3. **AdminUsersService** (`shared/services/admin/admin-users.service.ts`)
   - getUsers(params)

4. **AdminTeachersService** (`shared/services/admin/admin-teachers.service.ts`)
   - getTeachers(params)

5. **AdminCoursesService** (`shared/services/admin/admin-courses.service.ts`)
   - getCourses(params)

## HTTP Interceptors

Both interceptors are registered globally:

1. **JwtInterceptor** - Adds `Authorization: Bearer {token}` to all requests
2. **ErrorInterceptor** - Handles 401 errors, auto-logout on authentication failure

## Next Steps

After testing, you can:
1. Create proper UI components for each admin function
2. Add form validation
3. Implement route guards
4. Add loading skeletons
5. Create tables instead of JSON display
6. Add CRUD operations (create, update, delete)
