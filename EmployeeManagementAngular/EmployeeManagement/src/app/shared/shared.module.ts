import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterModule } from '@angular/router'; // Add RouterModule
import { NavbarComponent } from './components/navbar/navbar.component';
import { PasswordValidatorDirective } from './services/password-validator.directive';

@NgModule({
  declarations: [NavbarComponent, PasswordValidatorDirective],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [NavbarComponent, PasswordValidatorDirective]
})
export class SharedModule {}
