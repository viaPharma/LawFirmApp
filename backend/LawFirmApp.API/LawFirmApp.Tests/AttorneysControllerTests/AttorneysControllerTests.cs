using LawFirmApp.API.Controllers;
using LawFirmApp.Models;
using LawFirmApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LawFirmApp.Tests.Controllers
{
    public class AttorneysControllerTests
    {
        private readonly Mock<IAttorneyRepository> _mockRepo;
        private readonly AttorneysController _controller;

        public AttorneysControllerTests()
        {
            _mockRepo = new Mock<IAttorneyRepository>();
            _controller = new AttorneysController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfAttorneys()
        {
            // Arrange
            var attorneys = new List<Attorney>
            {
                new Attorney { Id = 1, Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890" },
                new Attorney { Id = 2, Name = "Jane Doe", Email = "jane@example.com", PhoneNumber = "987-654-3210" }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(attorneys);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Attorney>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithAttorney()
        {
            // Arrange
            var attorney = new Attorney { Id = 1, Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(attorney);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Attorney>(okResult.Value);
            Assert.Equal(attorney.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenAttorneyDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Attorney)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task AddAttorney_ReturnsCreatedAtAction_WithNewAttorney()
        {
            // Arrange
            var newAttorney = new Attorney { Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890" };
            var createdAttorney = new Attorney { Id = 1, Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890" };
            _mockRepo.Setup(repo => repo.AddAsync(newAttorney)).ReturnsAsync(createdAttorney);

            // Act
            var result = await _controller.AddAttorney(newAttorney);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Attorney>(createdAtActionResult.Value);
            Assert.Equal(createdAttorney.Id, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var updatedAttorney = new Attorney { Id = 1, Name = "John Doe Updated", Email = "john@example.com", PhoneNumber = "123-456-7890" };
            _mockRepo.Setup(repo => repo.UpdateAsync(updatedAttorney)).ReturnsAsync(updatedAttorney);

            // Act
            var result = await _controller.Update(1, updatedAttorney);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Attorney>(okResult.Value);
            Assert.Equal(updatedAttorney.Name, returnValue.Name);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenAttorneyDoesNotExist()
        {
            // Arrange
            var updatedAttorney = new Attorney { Id = 1, Name = "John Doe Updated", Email = "john@example.com", PhoneNumber = "123-456-7890" };
            _mockRepo.Setup(repo => repo.UpdateAsync(updatedAttorney)).ReturnsAsync((Attorney)null);

            // Act
            var result = await _controller.Update(1, updatedAttorney);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenAttorneyDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
