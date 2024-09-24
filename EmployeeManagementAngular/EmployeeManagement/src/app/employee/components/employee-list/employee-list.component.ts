import { Component, OnInit } from '@angular/core';
import { Employee } from '../../models/employeeModel';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employeelist',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  filteredEmployees: Employee[] = [];
  searchTerm: string = '';
  sortField: string = 'name';
  message: string | null = null;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees() {
    this.employeeService.getAllEmployees().subscribe((data) => {
      this.employees = data;
      this.filteredEmployees = data; // Initialize filteredEmployees with all employees
      this.sortEmployees();
    });
  }

  filterEmployees() {
    const term = this.searchTerm.toLowerCase();
    this.filteredEmployees = this.employees.filter(employee =>
      employee.name.toLowerCase().includes(term) ||
      (employee.departmentName && employee.departmentName.toLowerCase().includes(term)) // Ensure departmentName is not undefined
    );
  }

  confirmDelete(id: number) {
    this.employeeService.confirm('Are you sure you want to delete this employee?').then((result) => {
      if (result) {
        this.deleteEmployee(id);
      }
    });
  }

  deleteEmployee(id: number) {
    this.employeeService.deleteEmployee(id).subscribe(() => {
      this.loadEmployees();
      this.message = 'Employee deleted successfully!';
      setTimeout(() => {
        this.message = null;
      }, 3000); // Message disappears after 3 seconds
    });
  }

  sortEmployees() {
    this.filteredEmployees.sort((a: Employee, b: Employee) => {
      if (this.sortField === 'name') {
        return a.name?.localeCompare(b.name || '') || 0;
      } else if (this.sortField === 'department') {
        return a.departmentName?.localeCompare(b.departmentName || '') || 0;
      } else if (this.sortField === 'hireDate') {
        const dateA = a.hireDate ? new Date(a.hireDate) : new Date(0);
        const dateB = b.hireDate ? new Date(b.hireDate) : new Date(0);
        return dateA.getTime() - dateB.getTime();
      }
      return 0;
    });
  }
}
