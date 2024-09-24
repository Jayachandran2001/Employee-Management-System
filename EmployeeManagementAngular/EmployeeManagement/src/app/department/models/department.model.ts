// // src/app/user/models/department.model.ts
// export interface Department {
//   departmentId: number;
//   departmentName: string;
//   departmentLead: string;
//   description: string;
//   createdAt?: Date;
//   updatedAt?: Date;
//   isActive?: boolean;
// }

export interface Department {
  departmentId: number;
  departmentName: string;
  departmentLead: string;
  description: string;
}


export interface DepartmentCreateDto {
  departmentName: string;
  departmentLead: string;
  description: string;
}
