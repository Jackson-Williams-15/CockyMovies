using CM.API.Controllers;
using CM.API.Data;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class OrderControllerTests
{
    private readonly Mock<ILogger<OrderController>> _mockLogger;
    private readonly AppDbContext _context;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _mockLogger = new Mock<ILogger<OrderController>>();

        // Set up in-memory database for testing
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database per test
            .Options;
        _context = new AppDbContext(options);

        // Seed data
        SeedTestData(_context);

        _controller = new OrderController(_context, _mockLogger.Object);
    }

    private void SeedTestData(AppDbContext context)
    {
        var movie = new Movie
        {
            Id = 1,
            Title = "Test Movie",
            Description = "Test Description",
            DateReleased = DateTime.Now.AddMonths(-2),
            RatingId = 1,
            Showtimes = new List<Showtime>()
        };

        var showtime = new Showtime
        {
            Id = 1,
            Movie = movie,
            StartTime = DateTime.Now.AddDays(1),
            Tickets = new List<Ticket>()
        };

        var order = new OrderResult
        {
            Id = 1,
            UserId = 1,
            ProcessedDate = DateTime.Now,
            Success = true,
            Details = "Order processed successfully.",
            TotalPrice = 50,
            Tickets = new List<OrderTicket>
            {
                new OrderTicket
                {
                    OrderTicketId = 1,
                    TicketId = 1,
                    Showtime = showtime,
                    Movie = movie,
                    Price = 25,
                    Quantity = 2
                }
            }
        };

        context.Movies.Add(movie);
        context.Showtime.Add(showtime);
        context.OrderResult.Add(order);
        context.SaveChanges();
    }

    [Fact]
    public async Task GetOrderDetails_ReturnsOk_WithOrderDetails()
    {
        // Arrange
        int orderId = 1;

        // Act
        var result = await _controller.GetOrderDetails(orderId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var order = okResult.Value as OrderResult;

        Assert.NotNull(order);
        Assert.Equal(orderId, order.Id);
    }

    [Fact]
    public async Task GetOrderDetails_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        // Arrange
        int orderId = 99; // Non-existent order ID

        // Act
        var result = await _controller.GetOrderDetails(orderId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetUserOrders_ReturnsOk_WithUserOrders()
    {
        // Arrange
        int userId = 1;

        // Act
        var result = await _controller.GetUserOrders(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var orders = okResult.Value as List<OrderResult>;

        Assert.NotNull(orders);
        Assert.Single(orders);
        Assert.Equal(userId, orders.First().UserId);
    }

    [Fact]
    public async Task GetUserOrders_ReturnsNotFound_WhenUserHasNoOrders()
    {
        // Arrange
        int userId = 99; // Non-existent user ID

        // Act
        var result = await _controller.GetUserOrders(userId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}