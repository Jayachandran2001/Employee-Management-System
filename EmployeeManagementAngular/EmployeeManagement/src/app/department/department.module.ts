import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentRoutingModule } from './department-routing.module';
import { DepartmentComponent } from './department.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DepartmentListComponent } from './components/department-list/department-list.component';
import { DepartmentCreateComponent } from './components/department-create/department-create.component';
import { DepartmentUpdateComponent } from './components/department-update/department-update.component';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
  declarations: [
    DepartmentComponent,
    DepartmentListComponent,
    DepartmentCreateComponent,
    DepartmentUpdateComponent
  ],
  imports: [
    CommonModule,
    DepartmentRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ]
})
export class DepartmentModule {}
