using EmployeeManagement.API.Controllers;
using EmployeeManagement.API.Services.Interface;
using EmployeeManagment.API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class DepartmentControllerTests
    {
        private readonly Mock<IDepartmentService> _departmentServiceMock;
        private readonly Mock<ILogger<DepartmentController>> _loggerMock;
        private readonly DepartmentController _controller;

        public DepartmentControllerTests()
        {
            _departmentServiceMock = new Mock<IDepartmentService>();
            _loggerMock = new Mock<ILogger<DepartmentController>>();
            _controller = new DepartmentController(_departmentServiceMock.Object, _loggerMock.Object);
        }


        //Create Department Test Cases
        [Fact]
        public async Task CreateDepartment_Returns201Created_OnSuccess()
        {
            // Arrange
            var departmentDto = new DepartmentCreateDto
            {
                DepartmentName = "IT",
                DepartmentLead = "Alice",
                Description = "Information Technology"
            };

            var newDepartment = new DepartmentResponseDto
            {
                DepartmentId = 1,
                DepartmentName = "IT",
                DepartmentLead = "Alice",
                Description = "Information Technology"
            };

            _departmentServiceMock.Setup(service => service.CreateDepartmentAsync(departmentDto))
                .ReturnsAsync(newDepartment);

            // Act
            var result = await _controller.CreateDepartment(departmentDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetDepartmentById", createdResult.ActionName);
            Assert.Equal(1, ((DepartmentResponseDto)createdResult.Value).DepartmentId);
        }


        [Fact]
        public async Task CreateDepartment_Returns500_OnException()
        {
            // Arrange
            var departmentDto = new DepartmentCreateDto
            {
                DepartmentName = "HR",
                DepartmentLead = "John",
                Description = "Human Resources"
            };

            _departmentServiceMock.Setup(service => service.CreateDepartmentAsync(departmentDto))
                .ThrowsAsync(new Exception("Error creating department"));

            // Act
            var result = await _controller.CreateDepartment(departmentDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }


        //UpdateDepartment Test Cases
        [Fact]
        public async Task UpdateDepartment_Returns200_OnSuccess()
        {
            // Arrange
            var departmentId = 1;
            var departmentUpdateDto = new DepartmentUpdateDto
            {
                DepartmentName = "Updated IT",
                DepartmentLead = "Alice Updated",
                Description = "Updated Information Technology"
            };

            var updatedDepartment = new DepartmentResponseDto
            {
                DepartmentId = departmentId,
                DepartmentName = "Updated IT",
                DepartmentLead = "Alice Updated",
                Description = "Updated Information Technology"
            };

            _departmentServiceMock.Setup(service => service.UpdateEmployeeAsync(departmentId, departmentUpdateDto))
                .ReturnsAsync(updatedDepartment);

            // Act
            var result = await _controller.UpdateDepartment(departmentId, departmentUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updatedDepartment, okResult.Value);
        }

        [Fact]
        public async Task UpdateDepartment_Returns500_OnException()
        {
            // Arrange
            var departmentId = 1;
            var departmentUpdateDto = new DepartmentUpdateDto
            {
                DepartmentName = "HR",
                DepartmentLead = "Bob",
                Description = "Human Resources"
            };

            _departmentServiceMock.Setup(service => service.UpdateEmployeeAsync(departmentId, departmentUpdateDto))
                .ThrowsAsync(new Exception("Error updating department"));

            // Act
            var result = await _controller.UpdateDepartment(departmentId, departmentUpdateDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }


        //GetAllDepartments Test cases
        [Fact]
        public async Task GetAllDepartment_Returns200_OnSuccess()
        {
            // Arrange
            var departments = new List<DepartmentResponseDto>
    {
        new DepartmentResponseDto { DepartmentId = 1, DepartmentName = "IT" },
        new DepartmentResponseDto { DepartmentId = 2, DepartmentName = "HR" }
    };

            _departmentServiceMock.Setup(service => service.GetAllDepartmentAsync())
                .ReturnsAsync(departments);

            // Act
            var result = await _controller.GetAllDepartment();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDepartments = Assert.IsAssignableFrom<List<DepartmentResponseDto>>(okResult.Value);
            Assert.Equal(2, returnedDepartments.Count);
        }



        //GetDepartment ById Test cases
        [Fact]
        public async Task GetDepartmentById_Returns200_OnSuccess()
        {
            // Arrange
            var departmentId = 1;
            var department = new DepartmentResponseDto
            {
                DepartmentId = departmentId,
                DepartmentName = "IT",
                DepartmentLead = "Alice"
            };

            _departmentServiceMock.Setup(service => service.GetDepartmentByIdAsync(departmentId))
                .ReturnsAsync(department);

            // Act
            var result = await _controller.GetDepartmentById(departmentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDepartment = Assert.IsType<DepartmentResponseDto>(okResult.Value);
            Assert.Equal(departmentId, returnedDepartment.DepartmentId);
        }

        [Fact]
        public async Task GetDepartmentById_Returns404_OnNotFound()
        {
            // Arrange
            var departmentId = 99; // Non-existing department ID
            _departmentServiceMock.Setup(service => service.GetDepartmentByIdAsync(departmentId))
                .ReturnsAsync((DepartmentResponseDto)null);

            // Act
            var result = await _controller.GetDepartmentById(departmentId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }


        //SoftDeleteDepartment Test cases
        [Fact]
        public async Task SoftDeleteDepartment_Returns204_OnSuccess()
        {
            // Arrange
            var departmentId = 1;
            _departmentServiceMock.Setup(service => service.SoftDeleteDepartmentAsync(departmentId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.SoftDeleteDepartment(departmentId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task SoftDeleteDepartment_Returns404_OnNotFound()
        {
            // Arrange
            var departmentId = 2; // Assuming department with ID 2 doesn't exist
            _departmentServiceMock.Setup(service => service.SoftDeleteDepartmentAsync(departmentId))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.SoftDeleteDepartment(departmentId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var errorResult = (NotFoundObjectResult)result;

            // Access the anonymous object's 'error' property
            var errorMessage = errorResult.Value.GetType().GetProperty("error")?.GetValue(errorResult.Value, null);

            Assert.Equal("Department not found", errorMessage);
        }



    }
}
