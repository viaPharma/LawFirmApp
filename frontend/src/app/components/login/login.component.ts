import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin(): void {
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        this.authService.setToken(response.token);

        // Redirect based on role
        const userRole = this.authService.getRole();
        if (userRole === 'Admin') {
          this.router.navigate(['/attorneys']); // Admin goes to Attorneys page
        } else if (userRole === 'User') {
          this.router.navigate(['/error']); // User goes to Error page
        }
      },
      error: () => {
        this.errorMessage = 'Invalid username or password.';
      },
    });
  }
}
