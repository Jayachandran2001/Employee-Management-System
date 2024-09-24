import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';
import { Department } from '../../../department/models/department.model';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {
  employeeForm: FormGroup;
  selectedFile: File | null = null;
  departments: Department[] = []; // Holds the list of departments

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router
  ) {
    this.employeeForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]], // This can stay if you want to use built-in email validation
      departmentId: [null, Validators.required],
      hireDate: [null, Validators.required],
      age: [null, [Validators.required, Validators.min(18)]],
      gender: ['', Validators.required],
      dateOfBirth: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.getDepartments(); // Load departments when component initializes
  }

  // Fetch departments from the service
  getDepartments(): void {
    this.employeeService.getDepartments().subscribe({
      next: (departments: Department[]) => {
        this.departments = departments;
      },
      error: (err) => {
        console.error('Error fetching departments:', err);
      }
    });
  }

  onFileSelected(event: any): void {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }

  onSubmit(): void {
    if (this.employeeForm.invalid) {
      return;
    }

    const formData = new FormData();
    formData.append('name', this.employeeForm.get('name')?.value);
    formData.append('email', this.employeeForm.get('email')?.value);
    formData.append('departmentId', this.employeeForm.get('departmentId')?.value);
    formData.append('hireDate', this.employeeForm.get('hireDate')?.value);
    formData.append('age', this.employeeForm.get('age')?.value);
    formData.append('gender', this.employeeForm.get('gender')?.value);
    formData.append('dateOfBirth', this.employeeForm.get('dateOfBirth')?.value);

    if (this.selectedFile) {
      formData.append('employeeImage', this.selectedFile);
    }

    this.employeeService.createEmployee(formData).subscribe({
      next: () => {
        this.router.navigate(['/employee']);
      },
      error: (err) => {
        console.error('Error creating employee:', err);
      }
    });
  }

  goBack() {
    this.router.navigate(['/employee/list']); // Adjust the path as needed
  }
}
