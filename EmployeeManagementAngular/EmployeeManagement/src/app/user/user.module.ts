import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserRoutingModule } from './user-routing.module';
import { PasswordValidatorDirective } from '../shared/services/password-validator.directive';
import { SharedModule } from '../shared/shared.module';


const userRoutes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'register', component: RegisterComponent }
];

@NgModule({
  declarations: [UserComponent, LoginComponent, RegisterComponent],
  imports: [CommonModule, FormsModule, HttpClientModule,UserRoutingModule, RouterModule.forChild(userRoutes), SharedModule],
  exports: [RouterModule, LoginComponent, RegisterComponent]
})
export class UserModule {}
