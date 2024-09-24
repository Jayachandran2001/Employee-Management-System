import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'user/login', pathMatch: 'full' }, // Default redirect
  { path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule) }, // Lazy-load UserModule
  { path: 'employee', loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule) }, // Lazy-load EmployeeModule
  { path: 'department', loadChildren: () => import('./department/department.module').then(m => m.DepartmentModule) }, // Lazy-load EmployeeModule
  { path: '**', redirectTo: 'user/login' } // Fallback route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
