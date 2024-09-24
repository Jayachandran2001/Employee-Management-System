// src/app/employee/services/employeeService.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employeeModel';
import { Department } from '../../department/models/department.model';

@Injectable({
    providedIn: 'root'
})
export class EmployeeService {
    private apiUrl = 'https://localhost:9001/api/Employee';
    private apiUrl_Dept = 'https://localhost:9001/api/Department';

    constructor(private http: HttpClient) {}

    getAllEmployees(): Observable<Employee[]> {
        return this.http.get<Employee[]>(`${this.apiUrl}/GetAllEmployees`);
    }

    getEmployeeById(id: number): Observable<Employee> {
        return this.http.get<Employee>(`${this.apiUrl}/GetEmployeeById/${id}`);
    }

    createEmployee(employeeData: FormData): Observable<any> {
      return this.http.post(`${this.apiUrl}/CreateEmployee`, employeeData);
    }


      // Update employee
  updateEmployee(id: number, formData: FormData): Observable<Employee> {
    return this.http.put<Employee>(`${this.apiUrl}/UpdateEmployee/${id}`, formData);
  }


    deleteEmployee(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/SoftDeleteEmployee/${id}`);
    }

    searchEmployees(searchTerm: string): Observable<Employee[]> {
        return this.http.get<Employee[]>(`${this.apiUrl}/search?searchTerm=${searchTerm}`);
    }

      // Fetch departments
  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.apiUrl_Dept}/GetAllDepartment`);
  }

  confirm(message: string): Promise<boolean> {
    return new Promise<boolean>((resolve) => {
      const confirmation = confirm(message);
      resolve(confirmation);
    });
  }

}
