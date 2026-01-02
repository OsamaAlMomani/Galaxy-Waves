# 🐋 Whale Platform — Phase 2 Documentation

## Phase 2 — Admin Monitoring APIs (Users, Teachers, Courses)

---

## 🎯 Purpose of Phase 2

Phase 2 transforms the Whale backend from *"it runs"* into *"it explains the system"*.

The goal is **observability and control** for administrators:
- View users, teachers, and courses at scale
- Support pagination, searching, and filtering
- Provide API responses that are **frontend‑ready**
- Ensure contracts are stable and documented

Phase 2 deliberately avoids:
- authentication & authorization
- business workflows
- UI implementation

Those belong to later phases.

---

## 🧱 Design Principles Used

### 1. Read‑Only Admin APIs
Admin endpoints in Phase 2 are:
- query‑only
- non‑mutating
- safe to expose internally

This avoids accidental state corruption.

### 2. DTO‑First Responses
Admin APIs **never return EF entities**.
Instead, they return explicit DTOs designed for dashboards.

This gives:
- predictable schemas
- decoupling from persistence
- easier frontend consumption

### 3. Pagination by Default
All list endpoints:
- are paginated
- limit max page size
- prevent full‑table scans

This makes Whale scalable from day one.

---

## 📦 Shared Infrastructure (Used by All Admin Endpoints)

### PagedResult<T>

**Location:** `Whale.Application/Common/PagedResult.cs`

Purpose:
- standardize paginated responses
- allow UI reuse
- keep API shape consistent

Structure:
- current page
- page size
- total record count
- items list

---

### AdminListQuery

**Location:** `Whale.Application/Admin/Queries/AdminListQuery.cs`

Purpose:
- unify query parameters
- avoid duplicated query models

Supported parameters:
- page
- pageSize
- search
- status

Used across:
- Users
- Teachers
- Courses

---

## 👥 Admin Users Endpoint

### Endpoint
```
GET /api/admin/users
```

### Purpose
Allow admins to:
- list all users
- search by email or name
- filter by status
- paginate safely

### DTO
**Location:** `Whale.Application/Admin/Dto/UserListItemDto.cs`

Fields:
- Id
- Email
- FullName
- Role
- Status
- CreatedAtUtc
- LastSeenAtUtc

### Behavior
- Sorted by newest users first
- Search matches email or full name
- Status filter matches enum name

### Example queries
```
/api/admin/users?page=1&pageSize=20
/api/admin/users?search=admin
/api/admin/users?status=Active
```

---

## 🧑‍🏫 Admin Teachers Endpoint

### Endpoint
```
GET /api/admin/teachers
```

### Purpose
Allow admins to:
- monitor teacher lifecycle
- see verification status
- identify pending approvals

### DTO
**Location:** `Whale.Application/Admin/Dto/TeacherListItemDto.cs`

Fields:
- TeacherProfileId
- UserId
- Email
- FullName
- VerificationStatus
- JoinedAtUtc
- LeftAtUtc

### Behavior
- Includes related User data
- Search matches email or name
- Status filters verification state

### Example queries
```
/api/admin/teachers
/api/admin/teachers?status=Pending
/api/admin/teachers?search=teach
```

---

## 📚 Admin Courses Endpoint

### Endpoint
```
GET /api/admin/courses
```

### Purpose
Allow admins to:
- monitor course inventory
- distinguish draft vs published content
- link courses to teachers

### DTO
**Location:** `Whale.Application/Admin/Dto/CourseListItemDto.cs`

Fields:
- Id
- Title
- Status
- Price
- CreatedAtUtc
- TeacherUserId
- TeacherEmail
- TeacherFullName

### Behavior
- Sorted by creation date
- Search matches course title
- Status filters lifecycle state

### Example queries
```
/api/admin/courses
/api/admin/courses?status=Published
/api/admin/courses?search=Intro
```

---

## 🧪 Swagger & API Contracts

Swagger is the **source of truth** for Phase 2 APIs.

Available at:
```
http://localhost:8080/swagger
```

Swagger confirms:
- DTO schemas
- paged response models
- query parameters

The presence of schemas like:
- UserListItemDtoPagedResult
- TeacherListItemDtoPagedResult
- CourseListItemDtoPagedResult

means Phase 2 APIs are correctly exposed.

---

## 🛑 Common Pitfalls & How We Avoided Them

### Returning EF entities
❌ Causes tight coupling and serialization issues

✔ Fixed by explicit DTO projection

---

### Loading entire tables
❌ Breaks scalability

✔ Fixed by mandatory pagination

---

### Inconsistent response shapes
❌ Complicates frontend logic

✔ Fixed by shared PagedResult wrapper

---

## ✅ Phase 2 Completion Criteria

Phase 2 backend is considered complete when:

- Users endpoint supports paging, search, status
- Teachers endpoint supports paging, search, verification status
- Courses endpoint supports paging, search, lifecycle status
- DTOs are used everywhere
- Swagger shows paged schemas

All criteria have been met.

---

## 🔜 What Comes After Phase 2

Phase 3 will introduce:
- authentication & roles
- admin dashboard UI (Angular HexaDash)
- write operations (approve teacher, archive course)

Phase 2 intentionally stops **before** business actions.

---

## 📌 Summary

Phase 2 gives Whale its **eyes**.

Admins can:
- observe
- analyze
- prepare decisions

Without risking system state.

This is the correct foundation for scaling into a real platform.

