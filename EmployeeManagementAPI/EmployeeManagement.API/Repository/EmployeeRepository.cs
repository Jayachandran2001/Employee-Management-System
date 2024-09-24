using AutoMapper;
using EmployeeManagement.API.DataAccess;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repository.Interface;
using EmployeeManagment.API.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementDbContext _context;
        private readonly string _imagesFolderPath;
        private readonly string _baseUrl;
        private readonly IMapper _mapper;

        public EmployeeRepository(EmployeeManagementDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            _baseUrl = configuration["AppSettings:BaseUrl"];
            _mapper = mapper;
        }


        public async Task<EmployeeResponseDto> CreateEmployeeAsync(EmployeeCreateDto employeeDto)
        {
            try
            {
                var existingEmployee = await _context.Employees
                    .Where(e => e.Name == employeeDto.Name && e.Email == employeeDto.Email && 
                    e.IsActive == false) //newly added

                    .FirstOrDefaultAsync();

                if (existingEmployee != null)
                {
                    throw new EmployeeAlreadyExistsException("The employee already exists with the same name and email.");
                }

                if (!Directory.Exists(_imagesFolderPath))
                {
                    Directory.CreateDirectory(_imagesFolderPath);
                }

                string employeeImagePath = null;

                if (employeeDto.EmployeeImage != null)
                {
                    employeeImagePath = Path.Combine(_imagesFolderPath, Path.GetFileName(employeeDto.EmployeeImage.FileName));
                    using (var stream = new FileStream(employeeImagePath, FileMode.Create))
                    {
                        await employeeDto.EmployeeImage.CopyToAsync(stream);
                    }
                }

                var newEmployee = _mapper.Map<Employee>(employeeDto);
                newEmployee.EmployeeImagePath = employeeImagePath != null
                    ? Path.GetRelativePath(_imagesFolderPath, employeeImagePath)
                    : null;
                newEmployee.CreatedAt = DateTime.UtcNow;
                newEmployee.UpdatedAt = DateTime.UtcNow;
                newEmployee.IsActive = true;

                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync(); 
              
                newEmployee.EmployeeCode = $"EC-{newEmployee.EmployeeId}";

                _context.Employees.Update(newEmployee);
                await _context.SaveChangesAsync();

                return _mapper.Map<EmployeeResponseDto>(newEmployee);
            }
            catch (EmployeeAlreadyExistsException)
            {
                throw; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating employee: " + ex.Message);
            }
        }


        public async Task<EmployeeResponseDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeDto)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    throw new KeyNotFoundException("Employee not found");
                }

                // Check for uniqueness of employee name and email.
                var existingEmployee = await _context.Employees
                    .Where(e => e.Name == employeeDto.Name && e.Email == employeeDto.Email && e.EmployeeId != id)
                    .FirstOrDefaultAsync();

                if (existingEmployee != null)
                {
                    throw new EmployeeAlreadyExistsException("Another employee already exists with the same name and email.");
                }

                // Update the EmployeeImage if provided.
                if (employeeDto.EmployeeImage != null)
                {
                    var employeeImagePath = Path.Combine(_imagesFolderPath, Path.GetFileName(employeeDto.EmployeeImage.FileName));
                    using (var stream = new FileStream(employeeImagePath, FileMode.Create))
                    {
                        await employeeDto.EmployeeImage.CopyToAsync(stream);
                    }
                    employee.EmployeeImagePath = Path.GetRelativePath(_imagesFolderPath, employeeImagePath);
                }

                // Update DepartmentId if provided and valid.
                if (employeeDto.DepartmentId.HasValue)
                {
                    var department = await _context.Departments.FindAsync(employeeDto.DepartmentId.Value);
                    if (department == null)
                    {
                        throw new KeyNotFoundException("Department not found");
                    }
                    employee.DepartmentId = employeeDto.DepartmentId.Value;
                }

                // Update other fields.
                if (!string.IsNullOrWhiteSpace(employeeDto.Name))
                {
                    employee.Name = employeeDto.Name;
                }

                if (!string.IsNullOrWhiteSpace(employeeDto.Email))
                {
                    employee.Email = employeeDto.Email;
                }

                if (employeeDto.HireDate.HasValue)
                {
                    employee.HireDate = employeeDto.HireDate;
                }

                if (employeeDto.Age.HasValue)
                {
                    employee.Age = employeeDto.Age;
                }

                if (!string.IsNullOrWhiteSpace(employeeDto.Gender))
                {
                    employee.Gender = employeeDto.Gender;
                }

                if (employeeDto.DateOfBirth.HasValue)
                {
                    employee.DateOfBirth = employeeDto.DateOfBirth;
                }

                // Update the timestamp.
                employee.UpdatedAt = DateTime.UtcNow;

                // Save changes.
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Return updated employee details.
                return _mapper.Map<EmployeeResponseDto>(employee);
            }
            catch (EmployeeAlreadyExistsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating employee: " + ex.Message);
            }
        }


        public async Task<IEnumerable<EmployeeResponseDto>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _context.Employees
                    .AsNoTracking()
                    .Include(e => e.Department)  
                    .Where(e => e.IsActive == true)
                    .ToListAsync();

                var employeeDtos = employees.Select(e =>
                {
                    var dto = _mapper.Map<EmployeeResponseDto>(e);
                    dto.DepartmentName = e.Department?.DepartmentName; 

                    if (!string.IsNullOrEmpty(e.EmployeeImagePath))
                    {
                        dto.EmployeeImagePath = $"{_baseUrl}/images/{Path.GetFileName(e.EmployeeImagePath)}";
                    }
                    return dto;
                }).ToList();

                return employeeDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employees: " + ex.Message);
            }
        }


        public async Task<EmployeeResponseDto> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _context.Employees
                    .AsNoTracking()
                    .Include(e => e.Department) 
                    .FirstOrDefaultAsync(e => e.EmployeeId == id);

                if (employee == null)
                    throw new KeyNotFoundException("Employee not found");

                var employeeDto = _mapper.Map<EmployeeResponseDto>(employee);
                employeeDto.DepartmentName = employee.Department?.DepartmentName;

                if (!string.IsNullOrEmpty(employee.EmployeeImagePath))
                {
                    employeeDto.EmployeeImagePath = $"{_baseUrl}/images/{Path.GetFileName(employee.EmployeeImagePath)}";
                }

                return employeeDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employee by ID: " + ex.Message);
            }
        }


        public async Task<bool> SoftDeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees
                .Where(e => e.EmployeeId == id && e.IsActive == true)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return false;
            }

            employee.IsActive = false;
            employee.UpdatedAt = DateTime.UtcNow;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string searchTerm)
        {
            try
            {
                var employees = await _context.Employees
                    .AsNoTracking()
                    .Include(e => e.Department)
                    .Where(e => e.IsActive == true &&
                                (e.EmployeeCode.Contains(searchTerm) ||
                                 e.Name.Contains(searchTerm) ||
                                 e.Email.Contains(searchTerm)))
                    .ToListAsync()
                    .ConfigureAwait(false);

                var employeeDtos = employees.Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeCode = e.EmployeeCode,
                    Name = e.Name,
                    Email = e.Email,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department?.DepartmentName,
                    HireDate = e.HireDate,
                    EmployeeImage = !string.IsNullOrEmpty(e.EmployeeImagePath)
                        ? $"{_baseUrl}/images/{Path.GetFileName(e.EmployeeImagePath)}"
                        : null 
                }).ToList();

                return employeeDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching employees.", ex);
            }
        }

    }
}
