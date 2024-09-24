using EmployeeManagement.API.Repository.Interface;
using EmployeeManagement.API.Services.Interface;
using EmployeeManagment.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeResponseDto> CreateEmployeeAsync(EmployeeCreateDto employeeDto)
        {
            return await _employeeRepository.CreateEmployeeAsync(employeeDto);
        }

        public async Task<EmployeeResponseDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeDto)
        {
            return await _employeeRepository.UpdateEmployeeAsync(id, employeeDto);
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }

        public async Task<EmployeeResponseDto> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public async Task<bool> SoftDeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.SoftDeleteEmployeeAsync(id);
        }

        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string searchTerm)
        {
            return await _employeeRepository.SearchEmployeesAsync(searchTerm);
        }
    }

}
