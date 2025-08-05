import { Component } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { LoginRequest } from '../../Models/auth.model';
import { Router, RouterLink } from '@angular/router';
import { RegisterComponent } from '../register/register.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RegisterComponent, RouterLink, CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  model: LoginRequest = { email: '', password: '' };
  loading = false;
  error: string | null = null;

  constructor(private auth: AuthService, private router: Router) {}

  submit() {
    if (!this.model.email || !this.model.password) {
      this.error = 'Email and password required';
      return;
    }
    this.error = null;
    this.loading = true;
    this.auth.login(this.model).subscribe({
      next: (res) => {
        this.loading = false;
        if (res.success) {
          this.router.navigate(['/patients']);
        } else {
          this.error = 'Invalid credentials';
        }
      },
      error: (e) => {
        this.loading = false;
        this.error =
          e?.error?.message || e?.error?.errors?.[0] || 'Login failed';
      },
    });
  }
}
