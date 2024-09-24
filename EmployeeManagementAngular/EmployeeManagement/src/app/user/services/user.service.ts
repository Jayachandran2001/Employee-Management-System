import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { LoginUserDTO, RegisterUserDTO, TokenResponse } from '../models/userModel';


@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'https://localhost:9001/api/Users';
  private loggedIn = false; // Track login status

  constructor(private http: HttpClient) {}

  register(user: RegisterUserDTO): Observable<any> {
    return this.http.post(`${this.baseUrl}/Register`, user);
  }

  login(user: LoginUserDTO): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(`${this.baseUrl}/Login`, user).pipe(tap(() => {
      this.loggedIn = true; // Set login status to true
    }));
  }

  logout(): Observable<any> {
    return this.http.post(`${this.baseUrl}/Logout`, {}).pipe(tap(() => {
      this.loggedIn = false; // Set login status to false
    }));
  }

  isLoggedIn(): boolean {
    return this.loggedIn; // Return the login status
  }
}
