import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5137/api/auth';

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, { username, password });
  }

  setToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  getRole(): string | null {
    const token = this.getToken();
    if (token) {
      const decodedToken: any = jwt_decode(token);
      return decodedToken['role'] || null; // Extract the role claim
    }
    return null;
  }
  
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem('authToken');
  }
}
