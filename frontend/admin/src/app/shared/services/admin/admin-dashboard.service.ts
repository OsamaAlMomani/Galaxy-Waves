import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { AdminDashboardDto } from '../../models/admin.models';

@Injectable({
  providedIn: 'root'
})
export class AdminDashboardService {
  private apiUrl = `${environment.apiUrl}/admin/dashboard`;

  constructor(private http: HttpClient) {}

  /**
   * Get admin dashboard statistics
   * Backend: GET /api/admin/dashboard
   * Requires: Admin role
   */
  getDashboard(): Observable<AdminDashboardDto> {
    return this.http.get<AdminDashboardDto>(this.apiUrl);
  }
}
