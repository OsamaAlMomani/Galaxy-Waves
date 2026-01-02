import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../shared/services/authentication.service';

@Component({
  selector: 'app-login-test',
  templateUrl: './login-test.component.html',
  styleUrls: ['./login-test.component.css']
})
export class LoginTestComponent {
  email = 'admin@whale.com';
  password = 'admin123';
  loading = false;
  error: string | null = null;
  success: string | null = null;

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  onLogin(): void {
    this.loading = true;
    this.error = null;
    this.success = null;

    this.authService.login(this.email, this.password).subscribe({
      next: (user) => {
        this.success = `Login successful! Token: ${user.token?.substring(0, 20)}...`;
        this.loading = false;
        console.log('Logged in user:', user);
        
        // Navigate to admin test page after 2 seconds
        setTimeout(() => {
          this.router.navigate(['/dashboard/admin-test']);
        }, 2000);
      },
      error: (error) => {
        this.error = error.message || 'Login failed. Please check your credentials.';
        this.loading = false;
        console.error('Login error:', error);
      }
    });
  }

  onLogout(): void {
    this.authService.logout();
    this.success = 'Logged out successfully!';
    this.error = null;
  }

  getCurrentUser(): any {
    return this.authService.currentUserValue;
  }
}
