import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Department } from '../../models/department.model';
import { DepartmentService } from '../../services/department-service';

@Component({
  selector: 'app-department-update',
  templateUrl: './department-update.component.html',
  styleUrls: ['./department-update.component.css'],
})
export class DepartmentUpdateComponent implements OnInit {
  departmentForm!: FormGroup;
  departmentId!: number;

  constructor(
    private fb: FormBuilder,
    private departmentService: DepartmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.departmentId = +this.route.snapshot.paramMap.get('id')!; // Get the department ID from the route
    this.initializeForm();
    this.loadDepartmentDetails();  // Load department details when the component initializes
  }

  initializeForm() {
    this.departmentForm = this.fb.group({
      departmentName: [''],
      departmentLead: [''],
      description: [''],
    });
  }

  // Method to load existing department details
  loadDepartmentDetails() {
    this.departmentService.getDepartmentById(this.departmentId).subscribe(
      (department: Department) => {
        // Patch the form with the department details
        this.departmentForm.patchValue({
          departmentName: department.departmentName,
          departmentLead: department.departmentLead,
          description: department.description,
        });
      },
      (error: any) => {
        console.error('Error fetching department details', error);
      }
    );
  }

  updateDepartment() {
    const formData = new FormData();
    Object.keys(this.departmentForm.controls).forEach((key) => {
      formData.append(key, this.departmentForm.get(key)!.value);
    });

    this.departmentService.updateDepartment(this.departmentId, formData).subscribe(
      (response: any) => {
        console.log('Department updated successfully', response);
        this.router.navigate(['/department/list']);  // Navigate back to the list page after update
      },
      (error: any) => {
        console.error('Error updating department', error);
      }
    );
  }


  goBack() {
    this.router.navigate(['/department/list']); // Adjust the path as needed
  }
}
