import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username!: string;
  password!: string;

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    const loginPayload = {
      username: this.username,
      password: this.password
    };

    this.http.post<{ token: string }>('https://localhost:9001/api/Users/Login', loginPayload)
      .subscribe({
        next: (response) => {
          // Store the token in localStorage or sessionStorage
          localStorage.setItem('token', response.token);
          console.log('Login successful!', response);
          // Navigate to another page after successful login
          this.router.navigate(['/employee/list']); // Adjust to the correct path
        },
        error: (error) => {
          console.error('Login failed:', error); // Show an error message if needed
          alert('Login failed. Please check your credentials.');
        }
      });
  }
}
