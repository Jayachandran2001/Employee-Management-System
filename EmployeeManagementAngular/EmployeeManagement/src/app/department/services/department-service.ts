import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department, DepartmentCreateDto } from '../models/department.model';


@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private apiUrl = 'https://localhost:9001/api/Department'; // Adjust URL as necessary


  constructor(private http: HttpClient) {}

  getAllDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.apiUrl}/GetAllDepartment`);
  }

  // getDepartmentById(id: number): Observable<Department> {
  //   return this.http.get<Department>(`${this.apiUrl}/GetDepartmentById/${id}`);
  // }

  // // createDepartment(department: DepartmentCreate): Observable<Department> {
  // //   return this.http.post<Department>(`${this.apiUrl}/CreateDepartment`, department);
  // // }
  // createDepartment(departmentData: DepartmentCreateDto): Observable<any> {
  //   return this.http.post(`${this.apiUrl}/CreateDepartment`, departmentData);
  // }
  // createDepartment(department: Department): Observable<Department> {
  //   return this.http.post<Department>(`${this.apiUrl}/CreateDepartment`, department);
  // }
  updateDepartment(id: number, formData: FormData): Observable<Department> {
    return this.http.put<Department>(`${this.apiUrl}/UpdateDepartment/${id}`, formData);
  }
  getDepartmentById(id: number): Observable<Department> {
    return this.http.get<Department>(`${this.apiUrl}/GetDepartmentById/${id}`);
}


  createDepartment(departmentData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/CreateDepartment`, departmentData);
  }

  // updateDepartment(id: number, department: Department): Observable<void> {
  //   return this.http.put<void>(`${this.apiUrl}/UpdateDepartment/${id}`, department);
  // }

  deleteDepartment(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/SoftDeleteDepartment/${id}`);
  }
}
