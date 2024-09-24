import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeComponent } from './employee.component';
import { EmployeeCreateComponent } from './components/employee-create/employee-create.component';
import { EmployeeUpdateComponent } from './components/employee-update/employee-update.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { AuthGuard } from '../user/guards/auth.guard';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';



const routes: Routes = [
{ path: '', component: EmployeeComponent, canActivate: [AuthGuard], children: [
{ path: 'list', component: EmployeeListComponent },
{ path: 'create', component: EmployeeCreateComponent },
{ path: 'edit/:id', component: EmployeeUpdateComponent },
{ path: 'detail/:id', component: EmployeeDetailComponent },
{ path: '**', redirectTo: 'list' }
] }
];

// const routes: Routes = [
//   { path: '', component: EmployeeComponent, children: [
//     { path: 'list', component: EmployeeListComponent },
//     { path: 'create', component: EmployeeCreateComponent },
//     { path: 'edit/:id', component: EmployeeUpdateComponent },
//     { path: 'detail/:id', component: EmployeeDetailComponent },
//     { path: '**', redirectTo: 'list' }
//   ] }
// ];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule {}
