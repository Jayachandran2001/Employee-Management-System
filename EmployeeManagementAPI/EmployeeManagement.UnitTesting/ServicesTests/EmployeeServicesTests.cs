using EmployeeManagement.API.Repository.Interface;
using EmployeeManagement.API.Services;
using EmployeeManagement.API.Services.Interface;
using EmployeeManagment.API.DTO;
using Moq;
using Xunit;

namespace EmployeeManagement.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly IEmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateEmployeeAsync_ShouldReturnEmployeeResponseDto_WhenSuccessful()
        {
            // Arrange
            var employeeDto = new EmployeeCreateDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DepartmentId = 1,
                HireDate = DateTime.Now,
                Age = 30,
                Gender = "Male",
                DateOfBirth = new DateTime(1993, 5, 15)
            };
            var expectedResponse = new EmployeeResponseDto
            {
                EmployeeId = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                DepartmentId = 1,
                HireDate = DateTime.Now,
                Age = 30,
                Gender = "Male",
                DateOfBirth = new DateTime(1993, 5, 15),
                IsActive = true
            };

            _mockRepository.Setup(repo => repo.CreateEmployeeAsync(employeeDto))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _employeeService.CreateEmployeeAsync(employeeDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Name, result.Name);
        }

        [Fact]
        public async Task CreateEmployeeAsync_ShouldThrowException_WhenFailed()
        {
            // Arrange
            var employeeDto = new EmployeeCreateDto();
            _mockRepository.Setup(repo => repo.CreateEmployeeAsync(employeeDto))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _employeeService.CreateEmployeeAsync(employeeDto));
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldReturnEmployeeResponseDto_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            var employeeDto = new EmployeeUpdateDto
            {
                Name = "John Doe Updated",
                Email = "john.updated@example.com",
                DepartmentId = 1,
                HireDate = DateTime.Now,
                Age = 31,
                Gender = "Male",
                DateOfBirth = new DateTime(1993, 5, 15)
            };
            var expectedResponse = new EmployeeResponseDto
            {
                EmployeeId = id,
                Name = "John Doe Updated",
                Email = "john.updated@example.com",
                DepartmentId = 1,
                HireDate = DateTime.Now,
                Age = 31,
                Gender = "Male",
                DateOfBirth = new DateTime(1993, 5, 15),
                IsActive = true
            };

            _mockRepository.Setup(repo => repo.UpdateEmployeeAsync(id, employeeDto))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _employeeService.UpdateEmployeeAsync(id, employeeDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Name, result.Name);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldThrowException_WhenFailed()
        {
            // Arrange
            var id = 1;
            var employeeDto = new EmployeeUpdateDto();
            _mockRepository.Setup(repo => repo.UpdateEmployeeAsync(id, employeeDto))
                .ThrowsAsync(new Exception("Update failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _employeeService.UpdateEmployeeAsync(id, employeeDto));
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnEmployeeResponseDtoList_WhenSuccessful()
        {
            // Arrange
            var expectedResponse = new List<EmployeeResponseDto>
            {
                new EmployeeResponseDto { EmployeeId = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new EmployeeResponseDto { EmployeeId = 2, Name = "Jane Doe", Email = "jane.doe@example.com" }
            };

            _mockRepository.Setup(repo => repo.GetAllEmployeesAsync())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Count, result.Count());
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldThrowException_WhenFailed()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllEmployeesAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _employeeService.GetAllEmployeesAsync());
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnEmployeeResponseDto_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            var expectedResponse = new EmployeeResponseDto
            {
                EmployeeId = id,
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(id))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _employeeService.GetEmployeeByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Name, result.Name);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldThrowException_WhenFailed()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(id))
                .ThrowsAsync(new Exception("Employee not found"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _employeeService.GetEmployeeByIdAsync(id));
        }

        [Fact]
        public async Task SoftDeleteEmployeeAsync_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(repo => repo.SoftDeleteEmployeeAsync(id))
                .ReturnsAsync(true);

            // Act
            var result = await _employeeService.SoftDeleteEmployeeAsync(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SoftDeleteEmployeeAsync_ShouldReturnFalse_WhenFailed()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(repo => repo.SoftDeleteEmployeeAsync(id))
                .ReturnsAsync(false);

            // Act
            var result = await _employeeService.SoftDeleteEmployeeAsync(id);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task SearchEmployeesAsync_ShouldReturnEmployeeDtoList_WhenSuccessful()
        {
            // Arrange
            var searchTerm = "John";
            var expectedResponse = new List<EmployeeDto>
            {
                new EmployeeDto { EmployeeId = 1, Name = "John Doe", Email = "john.doe@example.com" }
            };

            _mockRepository.Setup(repo => repo.SearchEmployeesAsync(searchTerm))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _employeeService.SearchEmployeesAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Count, result.Count());
        }

        [Fact]
        public async Task SearchEmployeesAsync_ShouldThrowException_WhenFailed()
        {
            // Arrange
            var searchTerm = "John";
            _mockRepository.Setup(repo => repo.SearchEmployeesAsync(searchTerm))
                .ThrowsAsync(new Exception("Search failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _employeeService.SearchEmployeesAsync(searchTerm));
        }
    }
}
