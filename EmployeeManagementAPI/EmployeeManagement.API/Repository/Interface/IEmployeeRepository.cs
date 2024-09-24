using EmployeeManagment.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<EmployeeResponseDto> CreateEmployeeAsync(EmployeeCreateDto employeeDto);
        Task<IEnumerable<EmployeeResponseDto>> GetAllEmployeesAsync();
        Task<EmployeeResponseDto> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string searchTerm);
        Task<bool> SoftDeleteEmployeeAsync(int id);
        Task<EmployeeResponseDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeDto);
    }
}