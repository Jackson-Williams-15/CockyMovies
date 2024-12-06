using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class TicketServiceTests
{
    private readonly Mock<AppDbContext> _mockContext;
    private readonly TicketService _ticketService;

    public TicketServiceTests()
    {
        _mockContext = new Mock<AppDbContext>();
        _ticketService = new TicketService(_mockContext.Object);
    }

    [Fact]
    public async Task AddTicket_ShouldAddTicket_WhenTicketDoesNotExist()
    {
        // Arrange
        var ticket = new Ticket { Id = 1, Price = 10.00m };
        _mockContext.Setup(x => x.Ticket.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Ticket, bool>>>(), default))
                    .ReturnsAsync(false);

        // Act
        var result = await _ticketService.AddTicket(ticket);

        // Assert
        Assert.True(result);
        _mockContext.Verify(x => x.Ticket.Add(ticket), Times.Once);
        _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task AddTicket_ShouldNotAddTicket_WhenTicketAlreadyExists()
    {
        // Arrange
        var ticket = new Ticket { Id = 1, Price = 10.00m };
        _mockContext.Setup(x => x.Ticket.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Ticket, bool>>>(), default))
                    .ReturnsAsync(true);

        // Act
        var result = await _ticketService.AddTicket(ticket);

        // Assert
        Assert.False(result);
        _mockContext.Verify(x => x.Ticket.Add(It.IsAny<Ticket>()), Times.Never);
        _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }

    [Fact]
    public async Task EditTicket_ShouldUpdateTicket_WhenTicketExists()
    {
        // Arrange
        var existingTicket = new Ticket { Id = 1, Price = 10.00m };
        var updatedTicket = new Ticket { Id = 1, Price = 15.00m };

        _mockContext.Setup(x => x.Ticket.FindAsync(1))
                    .ReturnsAsync(existingTicket);

        // Act
        var result = await _ticketService.EditTicket(1, updatedTicket);

        // Assert
        Assert.True(result);
        Assert.Equal(15.00m, existingTicket.Price);
        _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task EditTicket_ShouldReturnFalse_WhenTicketDoesNotExist()
    {
        // Arrange
        _mockContext.Setup(x => x.Ticket.FindAsync(1))
                    .ReturnsAsync((Ticket)null);

        var updatedTicket = new Ticket { Id = 1, Price = 15.00m };

        // Act
        var result = await _ticketService.EditTicket(1, updatedTicket);

        // Assert
        Assert.False(result);
        _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Never);
    }
}
