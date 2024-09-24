using EmployeeManagement.API.DataAccess;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repository.Interface;
using EmployeeManagment.API.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public DepartmentRepository(EmployeeManagementDbContext context)
        {
            _context = context;
        }

        public async Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto departmentDTO)
        {
            try
            {
                var newDepartment = new Department
                {
                    DepartmentName = departmentDTO.DepartmentName,
                    Description = departmentDTO.Description,
                    DepartmentLead = departmentDTO.DepartmentLead,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                _context.Departments.Add(newDepartment);
                await _context.SaveChangesAsync();

                return new DepartmentResponseDto
                {
                    DepartmentId = newDepartment.DepartmentId,
                    DepartmentName = newDepartment.DepartmentName,
                    DepartmentLead = newDepartment.DepartmentLead,
                    Description = newDepartment.Description,
                    CreatedAt = newDepartment.CreatedAt,
                    IsActive = newDepartment.IsActive
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Department: " + ex.Message);
            }
        }

        public async Task<DepartmentResponseDto> UpdateDepartmentAsync(int id, DepartmentUpdateDto departmentDTO)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null) throw new KeyNotFoundException("Department not found");

                // Update fields if provided
                department.DepartmentName = departmentDTO.DepartmentName ?? department.DepartmentName;
                department.Description = departmentDTO.Description ?? department.Description;
                department.DepartmentLead = departmentDTO.DepartmentLead ?? department.DepartmentLead;

                // Update the updated timestamp
                department.UpdatedAt = DateTime.UtcNow;

                _context.Entry(department).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new DepartmentResponseDto
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                    DepartmentLead = department.DepartmentLead,
                    Description = department.Description,
                    CreatedAt = department.CreatedAt,
                    UpdatedAt = department.UpdatedAt,
                    IsActive = department.IsActive
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Department: " + ex.Message);
            }
        }

        public async Task<IEnumerable<DepartmentResponseDto>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _context.Departments
                    .Where(e => e.IsActive == true)
                    .ToListAsync();

                return departments.Select(department => new DepartmentResponseDto
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                    DepartmentLead = department.DepartmentLead,
                    Description = department.Description,
                    IsActive = department.IsActive,
                    CreatedAt = department.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Departments: " + ex.Message);
            }
        }

        public async Task<DepartmentResponseDto> GetDepartmentByIdAsync(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null) throw new KeyNotFoundException("Department not found");

                return new DepartmentResponseDto
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                    DepartmentLead = department.DepartmentLead,
                    Description = department.Description,
                    CreatedAt = department.CreatedAt,
                    UpdatedAt = department.UpdatedAt,
                    IsActive = department.IsActive
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving department by ID: " + ex.Message);
            }
        }

        public async Task<bool> SoftDeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments
                .Where(e => e.DepartmentId == id && e.IsActive == true)
                .FirstOrDefaultAsync();

            if (department == null)
            {
                return false;
            }

            department.IsActive = false;
            department.UpdatedAt = DateTime.UtcNow;

            _context.Departments.Update(department);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
