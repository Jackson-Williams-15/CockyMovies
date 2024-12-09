using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class TicketsControllerTests
{
    private readonly Mock<ITicketService> _mockTicketService;
    private readonly TicketsController _controller;

    public TicketsControllerTests()
    {
        _mockTicketService = new Mock<ITicketService>();
        _controller = new TicketsController(_mockTicketService.Object);
    }

    [Fact]
    public async Task AddTicket_ReturnsCreated_WhenTicketIsAddedSuccessfully()
    {
        // Arrange
        var movie = new Movie { Id = 1, Title = "Test Movie", Showtimes = new List<Showtime>() };
        var showtime = new Showtime { Id = 1, MovieId = 1, StartTime = System.DateTime.Now.AddHours(1), Movie = movie, Tickets = new List<Ticket>() };
        var ticket = new Ticket { Id = 1, Price = 10, ShowtimeId = 1, Showtime = showtime };
        _mockTicketService.Setup(s => s.AddTicket(It.IsAny<Ticket>())).ReturnsAsync(true);

        // Act
        var result = await _controller.AddTicket(ticket);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetTicketById), createdResult.ActionName);
        Assert.Equal(ticket, createdResult.Value);
    }

    [Fact]
    public async Task AddTicket_ReturnsBadRequest_WhenTicketIsNull()
    {
        // Act
        var result = await _controller.AddTicket(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Ticket cannot be null.", badRequestResult.Value);
    }

    [Fact]
    public async Task AddTicket_ReturnsBadRequest_WhenTicketAlreadyExistsOrInvalidData()
    {
        // Arrange
        var movie = new Movie { Id = 1, Title = "Test Movie", Showtimes = new List<Showtime>() };
        var showtime = new Showtime { Id = 1, MovieId = 1, StartTime = System.DateTime.Now.AddHours(1), Movie = movie, Tickets = new List<Ticket>() };
        var ticket = new Ticket { Id = 1, Price = 10, ShowtimeId = 1, Showtime = showtime };
        _mockTicketService.Setup(s => s.AddTicket(It.IsAny<Ticket>())).ReturnsAsync(false);

        // Act
        var result = await _controller.AddTicket(ticket);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Ticket already exists or invalid data.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetAllTickets_ReturnsOk_WithListOfTickets()
    {
        // Arrange
        var movie = new Movie { Id = 1, Title = "Test Movie", Showtimes = new List<Showtime>() };
        var showtime = new Showtime { Id = 1, MovieId = 1, StartTime = System.DateTime.Now.AddHours(1), Movie = movie, Tickets = new List<Ticket>() };
        var tickets = new List<Ticket>
        {
            new Ticket { Id = 1, Price = 10, ShowtimeId = 1, Showtime = showtime },
            new Ticket { Id = 2, Price = 15, ShowtimeId = 1, Showtime = showtime }
        };
        _mockTicketService.Setup(s => s.GetAllTickets()).ReturnsAsync(tickets);

        // Act
        var result = await _controller.GetAllTickets();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTickets = Assert.IsType<List<Ticket>>(okResult.Value);
        Assert.Equal(2, returnedTickets.Count);
    }

    [Fact]
    public async Task GetTicketsByMovieId_ReturnsOk_WithListOfTickets()
    {
        // Arrange
        int movieId = 1;
        var movie = new Movie { Id = movieId, Title = "Test Movie", Showtimes = new List<Showtime>() };
        var showtime = new Showtime { Id = 1, MovieId = movieId, StartTime = System.DateTime.Now.AddHours(1), Movie = movie, Tickets = new List<Ticket>() };
        var tickets = new List<Ticket>
        {
            new Ticket { Id = 1, Price = 10, ShowtimeId = 1, Showtime = showtime },
            new Ticket { Id = 2, Price = 15, ShowtimeId = 1, Showtime = showtime }
        };
        _mockTicketService.Setup(s => s.GetTicketsByMovieId(movieId)).ReturnsAsync(tickets);

        // Act
        var result = await _controller.GetTicketsByMovieId(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTickets = Assert.IsType<List<Ticket>>(okResult.Value);
        Assert.Equal(2, returnedTickets.Count);
    }

    [Fact]
    public async Task GetTicketsByMovieId_ReturnsNotFound_WhenNoTicketsExist()
    {
        // Arrange
        int movieId = 1;
        _mockTicketService.Setup(s => s.GetTicketsByMovieId(movieId)).ReturnsAsync(new List<Ticket>());

        // Act
        var result = await _controller.GetTicketsByMovieId(movieId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetTicketById_ReturnsOk_WithTicketDetails()
    {
        // Arrange
        int ticketId = 1;
        var movie = new Movie { Id = 1, Title = "Test Movie", Showtimes = new List<Showtime>() };
        var showtime = new Showtime { Id = 1, MovieId = 1, StartTime = System.DateTime.Now.AddHours(1), Movie = movie, Tickets = new List<Ticket>() };
        var ticket = new Ticket { Id = ticketId, Price = 10, ShowtimeId = 1, Showtime = showtime };
        _mockTicketService.Setup(s => s.GetTicketById(ticketId)).ReturnsAsync(ticket);

        // Act
        var result = await _controller.GetTicketById(ticketId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTicket = Assert.IsType<Ticket>(okResult.Value);
        Assert.Equal(ticketId, returnedTicket.Id);
    }

    [Fact]
    public async Task GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist()
    {
        // Arrange
        int ticketId = 1;
        _mockTicketService.Setup(s => s.GetTicketById(ticketId)).ReturnsAsync((Ticket)null);

        // Act
        var result = await _controller.GetTicketById(ticketId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
