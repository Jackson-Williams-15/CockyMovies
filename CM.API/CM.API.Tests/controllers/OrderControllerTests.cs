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
    private readonly Mock<ILogger<OrderController>> _mockLogger;  // Mock for ILogger
    private readonly AppDbContext _context;  // In-memory database context for testing
    private readonly OrderController _controller;  // The controller under test

    // Constructor to set up mock logger, in-memory database, and the controller instance
    public OrderControllerTests()
    {
        _mockLogger = new Mock<ILogger<OrderController>>();

        // Set up in-memory database for testing
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database per test
            .Options;
        _context = new AppDbContext(options);

        // Seed data into the in-memory database for the tests
        SeedTestData(_context);

        // Initialize the controller with the mock logger and in-memory context
        _controller = new OrderController(_context, _mockLogger.Object);
    }

    // Helper method to seed test data into the in-memory database
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

        // Add the data to the database
        context.Movies.Add(movie);
        context.Showtime.Add(showtime);
        context.OrderResult.Add(order);
        context.SaveChanges();
    }

    // Test for GetOrderDetails when the order is found
    [Fact]
    public async Task GetOrderDetails_ReturnsOk_WithOrderDetails()
    {
        // Arrange
        int orderId = 1; // ID of the existing order

        // Act: Call the GetOrderDetails method on the controller
        var result = await _controller.GetOrderDetails(orderId);

        // Assert: Verify that the result is Ok and contains the correct order
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok result
        var order = okResult.Value as OrderResult;  // Extract order from the response

        Assert.NotNull(order);  // Ensure the order is not null
        Assert.Equal(orderId, order.Id);  // Ensure the correct order ID is returned
    }

    // Test for GetOrderDetails when the order does not exist
    [Fact]
    public async Task GetOrderDetails_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        // Arrange
        int orderId = 99; // Non-existent order ID

        // Act: Call the GetOrderDetails method on the controller
        var result = await _controller.GetOrderDetails(orderId);

        // Assert: Verify that the result is NotFound since the order doesn't exist
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound result
    }

    // Test for GetUserOrders when the user has orders
    [Fact]
    public async Task GetUserOrders_ReturnsOk_WithUserOrders()
    {
        // Arrange
        int userId = 1; // ID of the existing user

        // Act: Call the GetUserOrders method on the controller
        var result = await _controller.GetUserOrders(userId);

        // Assert: Verify that the result is Ok and contains the user's orders
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok result
        var orders = okResult.Value as List<OrderResult>;  // Extract the list of orders

        Assert.NotNull(orders);  // Ensure orders are not null
        Assert.Single(orders);  // Ensure there's exactly one order for this user
        Assert.Equal(userId, orders.First().UserId);  // Ensure the order belongs to the correct user
    }

    // Test for GetUserOrders when the user has no orders
    [Fact]
    public async Task GetUserOrders_ReturnsNotFound_WhenUserHasNoOrders()
    {
        // Arrange
        int userId = 99; // Non-existent user ID

        // Act: Call the GetUserOrders method on the controller
        var result = await _controller.GetUserOrders(userId);

        // Assert: Verify that the result is NotFound since the user has no orders
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound result
    }
}
s