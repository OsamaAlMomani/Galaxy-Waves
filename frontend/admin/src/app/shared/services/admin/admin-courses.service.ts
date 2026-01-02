import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { PagedResult, CourseListItemDto, AdminListQuery } from '../../models/admin.models';

@Injectable({
  providedIn: 'root'
})
export class AdminCoursesService {
  private apiUrl = `${environment.apiUrl}/admin/courses`;

  constructor(private http: HttpClient) {}

  /**
   * Get paginated list of courses with optional search and status filter
   * Backend: GET /api/admin/courses?page=1&pageSize=20&search=angular&status=Published
   * Requires: Admin role
   */
  getCourses(query?: AdminListQuery): Observable<PagedResult<CourseListItemDto>> {
    let params = new HttpParams();
    
    if (query) {
      if (query.page) params = params.set('page', query.page.toString());
      if (query.pageSize) params = params.set('pageSize', query.pageSize.toString());
      if (query.search) params = params.set('search', query.search);
      if (query.status) params = params.set('status', query.status);
    }

    return this.http.get<PagedResult<CourseListItemDto>>(this.apiUrl, { params });
  }
}
