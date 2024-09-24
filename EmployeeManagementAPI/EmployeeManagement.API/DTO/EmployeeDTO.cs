using Microsoft.AspNetCore.Http;
using System;

namespace EmployeeManagment.API.DTO
{
    public class EmployeeCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public IFormFile EmployeeImage { get; set; }
        public DateTime? HireDate { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class EmployeeUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public IFormFile EmployeeImage { get; set; }
        public DateTime? HireDate { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public string EmployeeImagePath { get; set; }
        public DateTime? HireDate { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public string EmployeeImage { get; set; }
        public DateTime? HireDate { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string DepartmentName { get; set; }
    }
}