using EmployeeManagement.API.Models;
using EmployeeManagment.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Repository.Interface
{
    public interface IDepartmentRepository
    {
        Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto departmentDTO);
        Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync();
        Task<DepartmentResponseDto> GetDepartmentByIdAsync(int id);
        Task<bool> SoftDeleteDepartmentAsync(int id);
        Task<DepartmentResponseDto> UpdateDepartmentAsync(int id, DepartmentUpdateDto departmentDTO);
    }
}