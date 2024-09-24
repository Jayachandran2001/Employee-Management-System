using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repository.Interface;
using EmployeeManagement.API.Services.Interface;
using EmployeeManagment.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto departmentDTO)
        {
            return _departmentRepository.CreateDepartmentAsync(departmentDTO);
        }

        public Task<DepartmentResponseDto> UpdateEmployeeAsync(int id, DepartmentUpdateDto departmentDTO)
        {
            return _departmentRepository.UpdateDepartmentAsync(id, departmentDTO);
        }

        public Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentAsync()
        {
            return _departmentRepository.GetAllDepartmentsAsync();
        }

        public Task<DepartmentResponseDto> GetDepartmentByIdAsync(int id)
        {
            return _departmentRepository.GetDepartmentByIdAsync(id);
        }

        public async Task<bool> SoftDeleteDepartmentAsync(int id)
        {
            return await _departmentRepository.SoftDeleteDepartmentAsync(id);
        }
    }
}
