import { Injectable } from '@angular/core';
import {
  CanActivate,
  Router,
  UrlTree,
  NavigationExtras,
} from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) {}

  canActivate(): boolean | UrlTree {
    if (this.auth.getToken()) return true;
    // redirect to login with returnUrl
    return this.router.createUrlTree(['/login']);
  }
}
