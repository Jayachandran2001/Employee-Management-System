// src/app/employee/models/employeeModel.ts
export interface Employee {
  employeeId: number;
  employeeCode: string;
  name: string;
  email: string;
  departmentId?: number;
  departmentName?: string;
  hireDate?: Date;
  employeeImagePath?: string;
  age?: number;
  gender?: string;
  dateOfBirth?: Date;
  createdAt?: Date;
  updatedAt?: Date;
  isActive?: boolean;
}
