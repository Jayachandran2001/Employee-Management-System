import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartmentComponent } from './department.component';
import { AuthGuard } from '../user/guards/auth.guard';
import { DepartmentUpdateComponent } from './components/department-update/department-update.component';
import { DepartmentCreateComponent } from './components/department-create/department-create.component';
import { DepartmentListComponent } from './components/department-list/department-list.component';

const routes: Routes = [
  {
    path: '',
    component: DepartmentComponent,
    // canActivate: [AuthGuard],
    children: [
      { path: 'list', component: DepartmentListComponent },
      { path: 'create', component: DepartmentCreateComponent },
      { path: 'edit/:id', component: DepartmentUpdateComponent },
      { path: '**', redirectTo: 'list' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DepartmentRoutingModule {}
