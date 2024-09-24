using EmployeeManagement.API.Repository.Interface;
using EmployeeManagement.API.Services;
using EmployeeManagment.API.DTO;
using Moq;
using Xunit;

namespace EmployeeManagement.Tests.Services
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
        private readonly DepartmentService _departmentService;

        public DepartmentServiceTests()
        {
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _departmentService = new DepartmentService(_departmentRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateDepartmentAsync_Success()
        {
            // Arrange
            var departmentDTO = new DepartmentCreateDto
            {
                DepartmentName = "HR",
                DepartmentLead = "John Doe",
                Description = "Handles HR tasks"
            };

            var departmentResponse = new DepartmentResponseDto
            {
                DepartmentId = 1,
                DepartmentName = "HR",
                DepartmentLead = "John Doe",
                Description = "Handles HR tasks",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            _departmentRepositoryMock
                .Setup(repo => repo.CreateDepartmentAsync(departmentDTO))
                .ReturnsAsync(departmentResponse);

            // Act
            var result = await _departmentService.CreateDepartmentAsync(departmentDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HR", result.DepartmentName);
            _departmentRepositoryMock.Verify(repo => repo.CreateDepartmentAsync(departmentDTO), Times.Once);
        }

        [Fact]
        public async Task CreateDepartmentAsync_Failure()
        {
            // Arrange
            var departmentDTO = new DepartmentCreateDto
            {
                DepartmentName = null, // Invalid input
                DepartmentLead = "John Doe",
                Description = "Handles HR tasks"
            };

            _departmentRepositoryMock
                .Setup(repo => repo.CreateDepartmentAsync(departmentDTO))
                .ThrowsAsync(new Exception("Invalid department data"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _departmentService.CreateDepartmentAsync(departmentDTO));
            _departmentRepositoryMock.Verify(repo => repo.CreateDepartmentAsync(departmentDTO), Times.Once);
        }

        [Fact]
        public async Task UpdateDepartmentAsync_Success()
        {
            // Arrange
            var departmentId = 1;
            var departmentDTO = new DepartmentUpdateDto
            {
                DepartmentName = "HR Updated",
                DepartmentLead = "Jane Doe",
                Description = "Handles HR tasks with updates"
            };

            var departmentResponse = new DepartmentResponseDto
            {
                DepartmentId = departmentId,
                DepartmentName = "HR Updated",
                DepartmentLead = "Jane Doe",
                Description = "Handles HR tasks with updates",
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            _departmentRepositoryMock
                .Setup(repo => repo.UpdateDepartmentAsync(departmentId, departmentDTO))
                .ReturnsAsync(departmentResponse);

            // Act
            var result = await _departmentService.UpdateEmployeeAsync(departmentId, departmentDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HR Updated", result.DepartmentName);
            _departmentRepositoryMock.Verify(repo => repo.UpdateDepartmentAsync(departmentId, departmentDTO), Times.Once);
        }

        [Fact]
        public async Task UpdateDepartmentAsync_Failure()
        {
            // Arrange
            var departmentId = 1;
            var departmentDTO = new DepartmentUpdateDto
            {
                DepartmentName = null, // Invalid input
                DepartmentLead = "Jane Doe",
                Description = "Handles HR tasks with updates"
            };

            _departmentRepositoryMock
                .Setup(repo => repo.UpdateDepartmentAsync(departmentId, departmentDTO))
                .ThrowsAsync(new Exception("Invalid department data"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _departmentService.UpdateEmployeeAsync(departmentId, departmentDTO));
            _departmentRepositoryMock.Verify(repo => repo.UpdateDepartmentAsync(departmentId, departmentDTO), Times.Once);
        }

        [Fact]
        public async Task GetAllDepartmentAsync_Success()
        {
            // Arrange
            var departments = new List<DepartmentResponseDto>
            {
                new DepartmentResponseDto { DepartmentId = 1, DepartmentName = "HR" },
                new DepartmentResponseDto { DepartmentId = 2, DepartmentName = "IT" }
            };

            _departmentRepositoryMock
                .Setup(repo => repo.GetAllDepartmentsAsync())
                .ReturnsAsync(departments);

            // Act
            var result = await _departmentService.GetAllDepartmentAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _departmentRepositoryMock.Verify(repo => repo.GetAllDepartmentsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllDepartmentAsync_EmptyList()
        {
            // Arrange
            var departments = new List<DepartmentResponseDto>();

            _departmentRepositoryMock
                .Setup(repo => repo.GetAllDepartmentsAsync())
                .ReturnsAsync(departments);

            // Act
            var result = await _departmentService.GetAllDepartmentAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _departmentRepositoryMock.Verify(repo => repo.GetAllDepartmentsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetDepartmentByIdAsync_Success()
        {
            // Arrange
            var departmentId = 1;
            var departmentResponse = new DepartmentResponseDto
            {
                DepartmentId = departmentId,
                DepartmentName = "HR"
            };

            _departmentRepositoryMock
                .Setup(repo => repo.GetDepartmentByIdAsync(departmentId))
                .ReturnsAsync(departmentResponse);

            // Act
            var result = await _departmentService.GetDepartmentByIdAsync(departmentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(departmentId, result.DepartmentId);
            _departmentRepositoryMock.Verify(repo => repo.GetDepartmentByIdAsync(departmentId), Times.Once);
        }

        [Fact]
        public async Task GetDepartmentByIdAsync_NotFound()
        {
            // Arrange
            var departmentId = 1;

            _departmentRepositoryMock
                .Setup(repo => repo.GetDepartmentByIdAsync(departmentId))
                .ReturnsAsync((DepartmentResponseDto)null); // Simulate not found

            // Act
            var result = await _departmentService.GetDepartmentByIdAsync(departmentId);

            // Assert
            Assert.Null(result);
            _departmentRepositoryMock.Verify(repo => repo.GetDepartmentByIdAsync(departmentId), Times.Once);
        }

        [Fact]
        public async Task SoftDeleteDepartmentAsync_Success()
        {
            // Arrange
            var departmentId = 1;

            _departmentRepositoryMock
                .Setup(repo => repo.SoftDeleteDepartmentAsync(departmentId))
                .ReturnsAsync(true);

            // Act
            var result = await _departmentService.SoftDeleteDepartmentAsync(departmentId);

            // Assert
            Assert.True(result);
            _departmentRepositoryMock.Verify(repo => repo.SoftDeleteDepartmentAsync(departmentId), Times.Once);
        }

        [Fact]
        public async Task SoftDeleteDepartmentAsync_Failure()
        {
            // Arrange
            var departmentId = 1;

            _departmentRepositoryMock
                .Setup(repo => repo.SoftDeleteDepartmentAsync(departmentId))
                .ReturnsAsync(false);

            // Act
            var result = await _departmentService.SoftDeleteDepartmentAsync(departmentId);

            // Assert
            Assert.False(result);
            _departmentRepositoryMock.Verify(repo => repo.SoftDeleteDepartmentAsync(departmentId), Times.Once);
        }
    }
}
