using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Models;


public partial class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? DepartmentId { get; set; }
    public DateTime? HireDate { get; set; }
    public string EmployeeImagePath { get; set; }
    public int? Age { get; set; }
    public string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
    public virtual Department Department { get; set; }
}