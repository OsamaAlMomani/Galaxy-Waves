import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { PagedResult, UserListItemDto, AdminListQuery } from '../../models/admin.models';

@Injectable({
  providedIn: 'root'
})
export class AdminUsersService {
  private apiUrl = `${environment.apiUrl}/admin/users`;

  constructor(private http: HttpClient) {}

  /**
   * Get paginated list of users with optional search and status filter
   * Backend: GET /api/admin/users?page=1&pageSize=20&search=john&status=Active
   * Requires: Admin role
   */
  getUsers(query?: AdminListQuery): Observable<PagedResult<UserListItemDto>> {
    let params = new HttpParams();
    
    if (query) {
      if (query.page) params = params.set('page', query.page.toString());
      if (query.pageSize) params = params.set('pageSize', query.pageSize.toString());
      if (query.search) params = params.set('search', query.search);
      if (query.status) params = params.set('status', query.status);
    }

    return this.http.get<PagedResult<UserListItemDto>>(this.apiUrl, { params });
  }
}
