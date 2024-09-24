import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeCreateComponent } from './components/employee-create/employee-create.component';
import { EmployeeUpdateComponent } from './components/employee-update/employee-update.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeComponent } from './employee.component';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeRoutingModule } from './employee-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    EmployeeUpdateComponent,
    EmployeeListComponent,
    EmployeeComponent,
    EmployeeDetailComponent,
    EmployeeCreateComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    EmployeeRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ]
})
export class EmployeeModule { }
