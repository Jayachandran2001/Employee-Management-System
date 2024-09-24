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
    public class EmployeeRepositoryTests
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly Mock<ILogger<EmployeeController>> _loggerMock;
        private readonly EmployeeController _controller;

        public EmployeeRepositoryTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _loggerMock = new Mock<ILogger<EmployeeController>>();
            _controller = new EmployeeController(_employeeServiceMock.Object, _loggerMock.Object);
        }


        //GetAllEmployees Test Cases
        [Fact]
        public async Task GetAllEmployees_Returns200Status_OnGetSuccess()
        {
            // Arrange
            var employees = new List<EmployeeResponseDto>
            {
                new EmployeeResponseDto { EmployeeId = 1, Name = "Sample Name1" },
                new EmployeeResponseDto { EmployeeId = 2, Name = "Sample Name2" }
            };
            _employeeServiceMock.Setup(service => service.GetAllEmployeesAsync()).ReturnsAsync(employees);

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var returnValue = Assert.IsType<List<EmployeeResponseDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }


        [Fact]
        public async Task GetAllEmployees_Returns404Status_OnGetNullResponse()
        {
            // Arrange
            _employeeServiceMock.Setup(service => service.GetAllEmployeesAsync()).ReturnsAsync((List<EmployeeResponseDto>)null);

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        //GetEmployeeById Test Cases
        [Fact]
        public async Task GetEmployeeById_Returns200Status_OnGetSuccess()
        {
            // Arrange
            var employee = new EmployeeResponseDto { EmployeeId = 1, Name = "Sample Name" };
            _employeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync(employee);

            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var returnValue = Assert.IsType<EmployeeResponseDto>(okResult.Value);
            Assert.Equal("Sample Name", returnValue.Name);
        }

        [Fact]
        public async Task GetEmployeeById_Returns404Status_OnGetNullResponse()
        {
            // Arrange
            _employeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync((EmployeeResponseDto)null);

            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        //Search Emplpyee Test Cases
        [Fact]
        public async Task SearchEmployees_Returns200Status_OnGetSuccess()
        {
            // Arrange
            var searchTerm = "Sample";
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto { EmployeeId = 1, Name = "Sample Name" }
            };
            _employeeServiceMock.Setup(service => service.SearchEmployeesAsync(searchTerm)).ReturnsAsync(employees);

            // Act
            var result = await _controller.SearchEmployees(searchTerm);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<EmployeeDto>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal(StatusCodes.Status200OK, returnValue.StatusCode);
            Assert.Single((IEnumerable<EmployeeDto>)returnValue.Value);
        }

        [Fact]
        public async Task SearchEmployees_Returns200Status_OnGetNullResponse()
        {
            // Arrange
            var searchTerm = "NonExistent";
            _employeeServiceMock.Setup(service => service.SearchEmployeesAsync(searchTerm)).ReturnsAsync(new List<EmployeeDto>());

            // Act
            var result = await _controller.SearchEmployees(searchTerm);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<EmployeeDto>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal(StatusCodes.Status200OK, returnValue.StatusCode);
            Assert.Empty((IEnumerable<EmployeeDto>)returnValue.Value);
        }


        //Soft Delete Employee Test Cases
        [Fact]
        public async Task SoftDeleteEmployee_Returns204Status_OnGetSuccess()
        {
            // Arrange
            int employeeId = 1;
            _employeeServiceMock.Setup(service => service.SoftDeleteEmployeeAsync(employeeId)).ReturnsAsync(true);

            // Act
            var result = await _controller.SoftDeleteEmployee(employeeId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task SoftDeleteEmployee_Returns404Status_OnGetNullResponse()
        {
            // Arrange
            int employeeId = 1;
            _employeeServiceMock.Setup(service => service.SoftDeleteEmployeeAsync(employeeId)).ReturnsAsync(false);

            // Act
            var result = await _controller.SoftDeleteEmployee(employeeId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }




        //Create Employee Test Cases
        [Fact]
        public async Task CreateEmployee_Returns200Status_OnGetSuccess()
        {
            // Arrange
            var employeeDto = new EmployeeCreateDto { Name = "Sample Name" };
            var createdEmployee = new EmployeeResponseDto { EmployeeId = 1, Name = "Sample Name" };
            _employeeServiceMock.Setup(service => service.CreateEmployeeAsync(employeeDto))
                .ReturnsAsync(createdEmployee);

            // Act
            var result = await _controller.CreateEmployee(employeeDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.Equal(createdEmployee, createdResult.Value);
        }
        [Fact]
        public async Task CreateEmployee_Returns409Status_OnConflict()
        {
            // Arrange
            var employeeDto = new EmployeeCreateDto { Name = "Sample Name" };
            var conflictException = new EmployeeAlreadyExistsException("Employee already exists.");

            _employeeServiceMock
                .Setup(service => service.CreateEmployeeAsync(employeeDto))
                .ThrowsAsync(conflictException);

            // Act
            var result = await _controller.CreateEmployee(employeeDto);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status409Conflict, conflictResult.StatusCode);

            // Since the returned object is an anonymous type, we use reflection to access its properties.
            var returnValue = conflictResult.Value;
            var messageProperty = returnValue.GetType().GetProperty("message").GetValue(returnValue, null);

            Assert.Equal("Employee already exists.", messageProperty);
        }


        [Fact]
        public async Task UpdateEmployee_Returns200Status_OnGetSuccess()
        {
            // Arrange
            var employeeId = 1;
            var employeeUpdateDto = new EmployeeUpdateDto
            {
                Name = "Sample Name",
                Email = "sample.name1@example.com",
                DepartmentId = 2,
                HireDate = DateTime.Now,
                Age = 30,
                Gender = "Male",
                DateOfBirth = new DateTime(1993, 2, 25)
            };

            var updatedEmployee = new EmployeeResponseDto
            {
                EmployeeId = employeeId,
                Name = "Sample Name",
                Email = "sample.name1@example.com",
                DepartmentId = 2,
                HireDate = DateTime.Now,
                Age = 30,
                Gender = "Male",
                DateOfBirth = new DateTime(1993, 2, 25)
            };

            _employeeServiceMock.Setup(service => service.UpdateEmployeeAsync(employeeId, employeeUpdateDto))
                .ReturnsAsync(updatedEmployee);

            // Act
            var result = await _controller.UpdateEmployee(employeeId, employeeUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EmployeeResponseDto>(okResult.Value);

            Assert.Equal(employeeId, returnValue.EmployeeId);
            Assert.Equal("Sample Name", returnValue.Name);
            Assert.Equal("sample.name@example.com", returnValue.Email);
        }


        [Fact]
        public async Task UpdateEmployee_Returns404Status_OnGetNullResponse()
        {
            // Arrange
            var employeeId = 99; // Non-existing employee ID
            var employeeUpdateDto = new EmployeeUpdateDto
            {
                Name = "Non Existing",
                Email = "non.existing@example.com"
            };

            _employeeServiceMock.Setup(service => service.UpdateEmployeeAsync(employeeId, employeeUpdateDto))
                .ThrowsAsync(new KeyNotFoundException($"Employee with ID {employeeId} not found"));

            // Act
            var result = await _controller.UpdateEmployee(employeeId, employeeUpdateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }



    }
}
