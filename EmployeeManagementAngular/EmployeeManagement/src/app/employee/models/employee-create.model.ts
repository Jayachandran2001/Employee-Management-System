// src/app/employee/models/employee-create.model.ts
export interface EmployeeCreateDto {
  name: string;
  email: string;
  departmentId?: number;
  employeeImage?: File;
  hireDate?: Date;
  age?: number;
  gender?: string;
  dateOfBirth?: Date;
}

