using EmployeeManagement.API.Services.Interface;
using EmployeeManagment.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeResponseDto>> CreateEmployee([FromForm] EmployeeCreateDto employeeDto)
        {
            try
            {
                var employee = await _employeeService.CreateEmployeeAsync(employeeDto);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
            }
            catch (EmployeeAlreadyExistsException ex)
            {
                _logger.LogWarning(ex, "Attempt to create a duplicate employee");
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new employee");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new employee record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> UpdateEmployee(int id, [FromForm] EmployeeUpdateDto employeeDto)
        {
            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
                return Ok(updatedEmployee);
            }
            catch (EmployeeAlreadyExistsException ex)
            {
                _logger.LogWarning(ex, $"Attempt to update employee with duplicate name and email (ID {id})");
                return Conflict(new { message = ex.Message });  
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Employee not found for update (ID {id})");
                return NotFound(new { message = ex.Message });           
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating employee with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating employee record");
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();

                if (employees == null || !employees.Any())  // Check for null or empty list
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all employees");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving employee records");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                    return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching employee with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving employee details");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteEmployee(int id)
        {
            try
            {
                var result = await _employeeService.SoftDeleteEmployeeAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting employee with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting employee");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchEmployees([FromQuery] string searchTerm)
        {
            try
            {
                var employees = await _employeeService.SearchEmployeesAsync(searchTerm);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while searching for employees with term: {searchTerm}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching employees");
            }
        }
    }
}
