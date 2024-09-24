using System;

namespace EmployeeManagment.API.DTO
{
    public class DepartmentCreateDto
    {
        public string DepartmentName { get; set; }
        public string DepartmentLead { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentUpdateDto
    {
        public string DepartmentName { get; set; }
        public string DepartmentLead { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentResponseDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentLead { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}


