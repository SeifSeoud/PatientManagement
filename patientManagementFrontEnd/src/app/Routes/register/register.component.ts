import { Component } from '@angular/core';
import { RegisterRequest } from '../../Models/auth.model';
import { AuthService } from '../../Services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  model: RegisterRequest = {
    email: '',
    password: '',
    personName: '',
    gender: 'Male',
  };
  loading = false;
  error: string | null = null;

  constructor(private auth: AuthService, private router: Router) {}

  submit() {
    if (!this.model.email || !this.model.password || !this.model.personName) {
      this.error = 'All fields required';
      return;
    }
    this.error = null;
    this.loading = true;
    this.auth.register(this.model).subscribe({
      next: (res) => {
        this.loading = false;
        if (res.success) {
          this.router.navigate(['/patients']);
        } else {
          this.error = 'Registration failed';
        }
      },
      error: (e) => {
        this.loading = false;
        this.error =
          e?.error?.message || e?.error?.errors?.[0] || 'Register failed';
      },
    });
  }
}
