/*
using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class TicketControllerTests
{
    private readonly Mock<ITicketService> _mockService;
    private readonly TicketController _controller;

    public TicketControllerTests()
    {
        _mockService = new Mock<ITicketService>();
        _controller = new TicketController(_mockService.Object);
    }

    [Fact]
    public async Task AddTicket_ShouldReturnOk_WhenTicketIsAdded()
    {
        // Arrange
        var ticket = new Ticket { Id = 1, Price = 10.00m };
        _mockService.Setup(x => x.AddTicket(ticket)).ReturnsAsync(true);

        // Act
        var result = await _controller.AddTicket(ticket);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        _mockService.Verify(x => x.AddTicket(ticket), Times.Once);
    }

    [Fact]
    public async Task AddTicket_ShouldReturnBadRequest_WhenTicketCannotBeAdded()
    {
        // Arrange
        var ticket = new Ticket { Id = 1, Price = 10.00m };
        _mockService.Setup(x => x.AddTicket(ticket)).ReturnsAsync(false);

        // Act
        var result = await _controller.AddTicket(ticket);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        _mockService.Verify(x => x.AddTicket(ticket), Times.Once);
    }

    [Fact]
    public async Task EditTicket_ShouldReturnOk_WhenTicketIsUpdated()
    {
        // Arrange
        var ticket = new Ticket { Id = 1, Price = 15.00m };
        _mockService.Setup(x => x.EditTicket(1, ticket)).ReturnsAsync(true);

        // Act
        var result = await _controller.EditTicket(1, ticket);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        _mockService.Verify(x => x.EditTicket(1, ticket), Times.Once);
    }

    [Fact]
    public async Task EditTicket_ShouldReturnNotFound_WhenTicketIsNotUpdated()
    {
        // Arrange
        var ticket = new Ticket { Id = 1, Price = 15.00m };
        _mockService.Setup(x => x.EditTicket(1, ticket)).ReturnsAsync(false);

        // Act
        var result = await _controller.EditTicket(1, ticket);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        _mockService.Verify(x => x.EditTicket(1, ticket), Times.Once);
    }
}
*/