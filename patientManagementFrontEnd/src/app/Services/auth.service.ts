import { Injectable } from '@angular/core';
import {
  AuthenticationResponse,
  LoginRequest,
  RegisterRequest,
} from '../Models/auth.model';
import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:62129/api/Auth';

  constructor(private http: HttpClient) {}
  register(req: RegisterRequest): Observable<AuthenticationResponse> {
    return this.http
      .post<AuthenticationResponse>(`${this.apiUrl}/register`, req)
      .pipe(
        tap((res) => {
          if (res.success && res.token) {
            localStorage.setItem('jwt', res.token);
            localStorage.setItem('personName', res.personName || '');
          }
        })
      );
  }

  login(req: LoginRequest): Observable<AuthenticationResponse> {
    return this.http
      .post<AuthenticationResponse>(`${this.apiUrl}/login`, req)
      .pipe(
        tap((res) => {
          if (res.success && res.token) {
            localStorage.setItem('jwt', res.token);
            localStorage.setItem('personName', res.personName || '');
          }
        })
      );
  }

  logout() {
    localStorage.removeItem('jwt');
    localStorage.removeItem('personName');
  }

  getToken(): string | null {
    return localStorage.getItem('jwt');
  }

  getPersonName(): string {
    return localStorage.getItem('personName') || '';
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
