import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRoles = route.data['expectedRole']; // Expected roles
    const userRole = this.authService.getRole(); // Get the user's role

    if (
      this.authService.isLoggedIn() &&
      (!expectedRoles || expectedRoles.includes(userRole))
    ) {
      return true;
    }

    this.router.navigate(['/login']); // Redirect to login if unauthorized
    return false;
  }
}
