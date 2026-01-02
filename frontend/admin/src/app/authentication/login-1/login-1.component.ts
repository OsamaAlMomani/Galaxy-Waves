import { Component } from '@angular/core'
import { FormBuilder, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import  socialIcons  from './../../../assets/data/pages/social-items.json';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { AuthenticationService } from '../../shared/services/authentication.service';

@Component({
    templateUrl: './login-1.component.html'
})

export class Login1Component {
  loginForm: FormGroup;
  isLoading = false;
  error = false;
  errorMessage = '';
  socialMediaButtons = socialIcons.socialMediaButtons;

  validateForm!: UntypedFormGroup;

  constructor(
    private fb: FormBuilder, 
    private router: Router, 
    private location: Location,
    private authService: AuthenticationService
  ) {}

  submitForm(): void {
    if (this.validateForm.valid) {
      this.isLoading = true;
      this.error = false;
      this.errorMessage = '';
      
      const email = this.validateForm.value.userName;
      const password = this.validateForm.value.password;
      
      this.authService.login(email, password).subscribe({
        next: (response) => {
          this.isLoading = false;
          this.router.navigate(['/dashboard/demo-one']);
        },
        error: (error) => {
          this.isLoading = false;
          this.error = true;
          this.errorMessage = error || 'Login failed. Please check your credentials.';
          console.error('Login failed:', error);
        }
      });
    } else {
      Object.values(this.validateForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() =>
      this.validateForm.controls.checkPassword.updateValueAndValidity()
    );
  }

  passwordVisible = false;
  password?: string;

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: ['hexadash@dm.com', [Validators.required]],
      password: ['123456', [Validators.required]],
      remember: [true],
    });
  }
}
