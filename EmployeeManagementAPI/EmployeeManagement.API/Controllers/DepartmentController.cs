using EmployeeManagement.API.Services.Interface;
using EmployeeManagment.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromForm] DepartmentCreateDto departmentDto)
        {
            try
            {
                var newDepartment = await _departmentService.CreateDepartmentAsync(departmentDto);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.DepartmentId }, newDepartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromForm] DepartmentUpdateDto departmentDTO)
        {
            try
            {
                var department = await _departmentService.UpdateEmployeeAsync(id, departmentDTO);
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating department with ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all departments");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                if (department == null) return NotFound();
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving department with ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteDepartment(int id)
        {
            var result = await _departmentService.SoftDeleteDepartmentAsync(id);
            if (!result)
            {
                return NotFound(new { error = "Department not found" });
            }

            return NoContent();
        }


    }
}
