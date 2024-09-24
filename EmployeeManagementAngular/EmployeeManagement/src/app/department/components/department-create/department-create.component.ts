import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DepartmentService } from '../../services/department-service';


@Component({
  selector: 'app-department-create',
  templateUrl: './department-create.component.html',
  styleUrls: ['./department-create.component.css']
})
export class DepartmentCreateComponent {
  departmentForm: FormGroup;

  constructor(private fb: FormBuilder, private departmentService: DepartmentService, private router: Router) {
    this.departmentForm = this.fb.group({
      departmentName: ['', Validators.required],
      departmentLead: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.departmentForm.invalid) {
      return;
    }

    const formData = new FormData();
    formData.append('departmentName', this.departmentForm.get('departmentName')?.value);
    formData.append('departmentLead', this.departmentForm.get('departmentLead')?.value);
    formData.append('description', this.departmentForm.get('description')?.value);

    this.departmentService.createDepartment(formData).subscribe({
      next: () => {
        this.router.navigate(['/department/list']); // Navigate to the department list
      },
      error: (err: any) => {
        console.error('Error creating department:', err);
      }
    });
  }


  goBack() {
    this.router.navigate(['/department/list']); // Adjust the path as needed
  }
}
