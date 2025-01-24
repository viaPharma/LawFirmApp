import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Attorney {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
}


@Injectable({
  providedIn: 'root', // Makes the service globally available
})
export class AttorneyService {
  private apiUrl = 'http://localhost:5137/api/attorneys'; // Replace with your API endpoint

  constructor(private http: HttpClient) {}

  // Get all attorneys
  getAttorneys(): Observable<Attorney[]> {
    return this.http.get<Attorney[]>(this.apiUrl);
  }

  // Add a new attorney
  addAttorney(attorney: Attorney): Observable<Attorney> {
    return this.http.post<Attorney>(this.apiUrl, attorney);
  }

  // Update an existing attorney
  updateAttorney(attorney: Attorney): Observable<Attorney> {
    return this.http.put<Attorney>(`${this.apiUrl}/${attorney.id}`, attorney);
  }

  // Delete an attorney
  deleteAttorney(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
