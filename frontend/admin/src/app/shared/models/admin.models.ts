// Common models used across admin APIs

export interface PagedResult<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface AdminListQuery {
  page?: number;
  pageSize?: number;
  search?: string;
  status?: string;
}

// Dashboard models
export interface AdminDashboardDto {
  totalUsers: number;
  activeUsers: number;
  totalTeachers: number;
  pendingTeachers: number;
  approvedTeachers: number;
  totalCourses: number;
  publishedCourses: number;
  draftCourses: number;
}

// User models
export interface UserListItemDto {
  id: string;
  email: string;
  fullName: string;
  role: string;
  status: string;
  createdAtUtc: string;
}

// Teacher models
export interface TeacherListItemDto {
  id: string;
  userId: string;
  email: string;
  fullName: string;
  bio: string;
  verificationStatus: string;
  joinedAtUtc: string;
}

// Course models
export interface CourseListItemDto {
  id: string;
  title: string;
  teacherName: string;
  status: string;
  enrollmentCount: number;
  createdAtUtc: string;
}
