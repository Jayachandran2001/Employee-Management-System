using EmployeeManagement.API.Models;
using EmployeeManagment.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Services.Interface
{
    public interface IDepartmentService
    {
        Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto departmentDTO);
        Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentAsync();
        Task<DepartmentResponseDto> GetDepartmentByIdAsync(int id);
        Task<bool> SoftDeleteDepartmentAsync(int id);
        Task<DepartmentResponseDto> UpdateEmployeeAsync(int id, DepartmentUpdateDto departmentDTO);
    }
}