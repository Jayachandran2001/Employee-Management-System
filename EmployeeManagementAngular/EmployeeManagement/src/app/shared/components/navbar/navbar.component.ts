import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../user/services/auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  constructor(public authService: AuthService, private router: Router) {}

  logout() {
    if (window.confirm('Are you sure you want to logout?')) {
      this.authService.logout();
      this.router.navigate(['/user/login']);
    }
  }
}
