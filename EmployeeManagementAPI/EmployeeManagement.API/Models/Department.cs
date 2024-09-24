using System;
using System.Collections.Generic;

namespace EmployeeManagement.API.Models;

public partial class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public string Description { get; set; }
    public string DepartmentLead { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}