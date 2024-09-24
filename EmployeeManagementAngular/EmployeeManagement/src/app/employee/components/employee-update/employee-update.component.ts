import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { Employee } from '../../models/employeeModel';
import { Department } from '../../../department/models/department.model';
// Assuming you have this model

@Component({
  selector: 'app-employee-update',
  templateUrl: './employee-update.component.html',
  styleUrls: ['./employee-update.component.css'],
})
export class EmployeeUpdateComponent implements OnInit {
  employeeForm!: FormGroup;
  employeeId!: number;
  departments: Department[] = [];

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.employeeId = +this.route.snapshot.paramMap.get('id')!; // Get the employee ID from the route
    this.initializeForm();
    this.loadEmployeeDetails();  // Load the employee details when the component initializes
    this.loadDepartments();      // Load departments for the dropdown
  }

  initializeForm() {
    this.employeeForm = this.fb.group({
      name: [''],
      email: [''],
      departmentId: [''],
      hireDate: [''],
      employeeImage: [null],
      age: [''],
      gender: [''],
      dateOfBirth: [''],
    });
  }

  // Method to load existing employee details
  loadEmployeeDetails() {
    this.employeeService.getEmployeeById(this.employeeId).subscribe(
      (employee: Employee) => {
        this.employeeForm.patchValue({
          name: employee.name,
          email: employee.email,
          departmentId: employee.departmentId,
          hireDate: employee.hireDate ? new Date(employee.hireDate).toISOString().split('T')[0] : '',
          age: employee.age,
          gender: employee.gender,
          dateOfBirth: employee.dateOfBirth ? new Date(employee.dateOfBirth).toISOString().split('T')[0] : '',
        });
      },
      (error) => {
        console.error('Error fetching employee details', error);
      }
    );
  }

  // Load departments to populate the dropdown
  loadDepartments() {
    this.employeeService.getDepartments().subscribe(
      (departments: Department[]) => {
        this.departments = departments;
      },
      (error) => {
        console.error('Error fetching departments', error);
      }
    );
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.employeeForm.patchValue({ employeeImage: file });
    }
  }

  updateEmployee() {
    const formData = new FormData();
    Object.keys(this.employeeForm.controls).forEach((key) => {
      formData.append(key, this.employeeForm.get(key)!.value);
    });

    this.employeeService.updateEmployee(this.employeeId, formData).subscribe(
      (response) => {
        console.log('Employee updated successfully', response);
        this.router.navigate(['/employee/list']);  // Navigate back to the list page after update
      },
      (error) => {
        console.error('Error updating employee', error);
      }
    );
  }

  goBack() {
    this.router.navigate(['/employee/list']); // Adjust the path as needed
  }
}
