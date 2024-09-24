import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Department } from '../../models/department.model';
import { DepartmentService } from '../../services/department-service';

@Component({
  selector: 'app-departmentlist',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements OnInit {
  departments: Department[] = [];
  filteredDepartments: Department[] = [];
  searchTerm: string = '';
  sortField: string = 'departmentName';

  constructor(private departmentService: DepartmentService, private router: Router) {}

  ngOnInit(): void {
    this.loadDepartments();
  }

  loadDepartments() {
    this.departmentService.getAllDepartments().subscribe(departments => {
      this.departments = departments;
      this.filteredDepartments = departments;
      this.sortDepartments();
    });
  }

  editDepartment(id: number) {
    this.router.navigate(['/department/edit', id]);
  }

  deleteDepartment(id: number) {
    // Confirmation dialog before deletion
    if (window.confirm('Are you sure you want to delete this department?')) {
      this.departmentService.deleteDepartment(id).subscribe(() => {
        this.loadDepartments();
      });
    }
  }

  sortDepartments() {
    this.departments.sort((a: Department, b: Department) => {
      if (this.sortField === 'departmentName') {
        return a.departmentName?.localeCompare(b.departmentName || '') || 0;
      } else if (this.sortField === 'description') {
        return a.description?.localeCompare(b.description || '') || 0;
      } else if (this.sortField === 'departmentLead') {
        return a.departmentLead?.localeCompare(b.departmentLead || '') || 0;
      }
      return 0;
    });
  }

  searchDepartments() {
    if (this.searchTerm) {
      this.filteredDepartments = this.departments.filter(department =>
        department.departmentName?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        department.description?.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    } else {
      this.filteredDepartments = this.departments;
    }
  }


  goBack() {
    this.router.navigate(['/department/list']); // Adjust the path as needed
  }
}
