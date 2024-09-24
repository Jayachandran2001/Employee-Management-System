import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterUserDTO } from '../models/userModel';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  user: RegisterUserDTO = { username: '', email: '', password: '', dateOfBirth: undefined };
  successMessage: string | null = null;
  errorMessage: string | null = null;
  today: string = new Date().toISOString().split('T')[0]; // Current date in 'yyyy-mm-dd' format

  constructor(private userService: UserService, private router: Router) {}

  register() {
    if (this.user.dateOfBirth && new Date(this.user.dateOfBirth) > new Date()) {
      this.errorMessage = 'Date of Birth cannot be in the future.';
      return;
    }

    this.userService.register(this.user).subscribe(
      (response) => {
        this.successMessage = 'User registered successfully';
        setTimeout(() => {
          this.router.navigate(['/']);
        }, 2000);
      },
      (error) => {
        this.errorMessage = 'Registration failed';
      }
    );
  }
}
