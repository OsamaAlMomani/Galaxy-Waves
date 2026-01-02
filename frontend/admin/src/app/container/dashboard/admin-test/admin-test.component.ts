import { Component, OnInit } from '@angular/core';
import { AdminDashboardService } from '../../../shared/services/admin/admin-dashboard.service';
import { AdminUsersService } from '../../../shared/services/admin/admin-users.service';
import { AdminTeachersService } from '../../../shared/services/admin/admin-teachers.service';
import { AdminCoursesService } from '../../../shared/services/admin/admin-courses.service';

@Component({
  selector: 'app-admin-test',
  templateUrl: './admin-test.component.html',
  styleUrls: ['./admin-test.component.css']
})
export class AdminTestComponent implements OnInit {
  // Dashboard
  dashboardData: any = null;
  dashboardLoading = false;
  dashboardError: string | null = null;

  // Users
  usersData: any = null;
  usersLoading = false;
  usersError: string | null = null;
  usersPage = 1;
  usersPageSize = 10;
  usersSearch = '';

  // Teachers
  teachersData: any = null;
  teachersLoading = false;
  teachersError: string | null = null;
  teachersPage = 1;
  teachersPageSize = 10;
  teachersSearch = '';

  // Courses
  coursesData: any = null;
  coursesLoading = false;
  coursesError: string | null = null;
  coursesPage = 1;
  coursesPageSize = 10;
  coursesSearch = '';

  constructor(
    private dashboardService: AdminDashboardService,
    private usersService: AdminUsersService,
    private teachersService: AdminTeachersService,
    private coursesService: AdminCoursesService
  ) {}

  ngOnInit(): void {
    this.loadDashboard();
  }

  // Dashboard Methods
  loadDashboard(): void {
    this.dashboardLoading = true;
    this.dashboardError = null;
    
    this.dashboardService.getDashboard().subscribe({
      next: (data) => {
        this.dashboardData = data;
        this.dashboardLoading = false;
      },
      error: (error) => {
        this.dashboardError = error.message || 'Failed to load dashboard';
        this.dashboardLoading = false;
        console.error('Dashboard error:', error);
      }
    });
  }

  // Users Methods
  loadUsers(): void {
    this.usersLoading = true;
    this.usersError = null;
    
    this.usersService.getUsers({
      page: this.usersPage,
      pageSize: this.usersPageSize,
      search: this.usersSearch || undefined
    }).subscribe({
      next: (data) => {
        this.usersData = data;
        this.usersLoading = false;
      },
      error: (error) => {
        this.usersError = error.message || 'Failed to load users';
        this.usersLoading = false;
        console.error('Users error:', error);
      }
    });
  }

  // Teachers Methods
  loadTeachers(): void {
    this.teachersLoading = true;
    this.teachersError = null;
    
    this.teachersService.getTeachers({
      page: this.teachersPage,
      pageSize: this.teachersPageSize,
      search: this.teachersSearch || undefined
    }).subscribe({
      next: (data) => {
        this.teachersData = data;
        this.teachersLoading = false;
      },
      error: (error) => {
        this.teachersError = error.message || 'Failed to load teachers';
        this.teachersLoading = false;
        console.error('Teachers error:', error);
      }
    });
  }

  // Courses Methods
  loadCourses(): void {
    this.coursesLoading = true;
    this.coursesError = null;
    
    this.coursesService.getCourses({
      page: this.coursesPage,
      pageSize: this.coursesPageSize,
      search: this.coursesSearch || undefined
    }).subscribe({
      next: (data) => {
        this.coursesData = data;
        this.coursesLoading = false;
      },
      error: (error) => {
        this.coursesError = error.message || 'Failed to load courses';
        this.coursesLoading = false;
        console.error('Courses error:', error);
      }
    });
  }
}
