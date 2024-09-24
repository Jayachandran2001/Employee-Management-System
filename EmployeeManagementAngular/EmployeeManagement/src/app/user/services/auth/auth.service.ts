// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = false;

  constructor(private router: Router) {}

  login(): boolean {
    this.loggedIn = true;
    return this.loggedIn;
  }

  logout() {
    this.loggedIn = false;
    localStorage.removeItem('token'); // Remove token on logout
    this.router.navigate(['/user/login']); // Redirect on logout
  }

  isLoggedIn(): boolean {
    return this.loggedIn || !!localStorage.getItem('token'); // Check token presence
  }
}
