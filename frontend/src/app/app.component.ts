import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = 'LawFirmApp';

  constructor(public authService: AuthService, private router: Router) {}

  /**
   * Handles user logout.
   * Clears the authentication token and navigates back to the login page.
   */
  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
