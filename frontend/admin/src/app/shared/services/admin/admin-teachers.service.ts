import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { PagedResult, TeacherListItemDto, AdminListQuery } from '../../models/admin.models';

@Injectable({
  providedIn: 'root'
})
export class AdminTeachersService {
  private apiUrl = `${environment.apiUrl}/admin/teachers`;

  constructor(private http: HttpClient) {}

  /**
   * Get paginated list of teachers with optional search and verification status filter
   * Backend: GET /api/admin/teachers?page=1&pageSize=20&search=jane&status=Pending
   * Requires: Admin role
   */
  getTeachers(query?: AdminListQuery): Observable<PagedResult<TeacherListItemDto>> {
    let params = new HttpParams();
    
    if (query) {
      if (query.page) params = params.set('page', query.page.toString());
      if (query.pageSize) params = params.set('pageSize', query.pageSize.toString());
      if (query.search) params = params.set('search', query.search);
      if (query.status) params = params.set('status', query.status);
    }

    return this.http.get<PagedResult<TeacherListItemDto>>(this.apiUrl, { params });
  }
}
