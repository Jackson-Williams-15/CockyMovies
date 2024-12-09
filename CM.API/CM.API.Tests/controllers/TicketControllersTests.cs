using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CM.API.Tests.Controllers
{
    public class TicketControllerTests
    {
        private readonly Mock<ITicketService> _mockService;
        private readonly TicketsController _controller;

        public TicketControllerTests()
        {
            _mockService = new Mock<ITicketService>();
            _controller = new TicketsController(_mockService.Object);
        }

        [Fact]
        public async Task AddTicket_ShouldReturnOk_WhenTicketIsAdded()
        {
            // Arrange
            var movie = new Movie { Id = 1, Title = "Some Movie", Showtimes = new List<Showtime>() };
            var showtime = new Showtime
            {
                Id = 1,
                StartTime = DateTime.Now,
                Movie = movie,  // Set the Movie
                Tickets = new List<Ticket>(),  // Initialize the Tickets collection
                Capacity = 100,
                TicketsAvailable = 100
            };
            movie.Showtimes.Add(showtime); // Add the showtime to the movie's showtimes list

            var ticket = new Ticket
            {
                Id = 1,
                Price = 10.00m,
                Showtime = showtime  // Set the Showtime property
            };

            _mockService.Setup(x => x.AddTicket(ticket)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddTicket(ticket);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            _mockService.Verify(x => x.AddTicket(ticket), Times.Once);
        }

        [Fact]
        public async Task AddTicket_ShouldReturnBadRequest_WhenTicketCannotBeAdded()
        {
            // Arrange
            var movie = new Movie { Id = 1, Title = "Some Movie", Showtimes = new List<Showtime>() };
            var showtime = new Showtime
            {
                Id = 1,
                StartTime = DateTime.Now,
                Movie = movie,
                Tickets = new List<Ticket>(), // Initialize the Tickets collection
                Capacity = 100,
                TicketsAvailable = 100
            };
            movie.Showtimes.Add(showtime); // Add the showtime to the movie's showtimes list

            var ticket = new Ticket
            {
                Id = 1,
                Price = 10.00m,
                Showtime = showtime
            };

            _mockService.Setup(x => x.AddTicket(ticket)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddTicket(ticket);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            _mockService.Verify(x => x.AddTicket(ticket), Times.Once);
        }

        [Fact]
        public async Task EditTicket_ShouldReturnOk_WhenTicketIsUpdated()
        {
            // Arrange
            var movie = new Movie { Id = 1, Title = "Updated Movie", Showtimes = new List<Showtime>() };
            var showtime = new Showtime
            {
                Id = 1,
                StartTime = DateTime.Now.AddHours(2),
                Movie = movie,
                Tickets = new List<Ticket>(),
                Capacity = 100,
                TicketsAvailable = 100
            };
            movie.Showtimes.Add(showtime); // Add the showtime to the movie's showtimes list

            var ticket = new Ticket
            {
                Id = 1,
                Price = 15.00m,
                Showtime = showtime
            };

            _mockService.Setup(x => x.EditTicket(1, ticket.Price)).ReturnsAsync(true);

            // Act
            var result = await _controller.EditTicketPrice(1, new EditTicketPriceDto { NewPrice = ticket.Price });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            _mockService.Verify(x => x.EditTicket(1, ticket.Price), Times.Once);
        }

        [Fact]
public async Task EditTicket_ShouldReturnNotFound_WhenTicketUpdateFails()
{
    // Arrange
    var movie = new Movie { Id = 1, Title = "Updated Movie", Showtimes = new List<Showtime>() };
    var showtime = new Showtime
    {
        Id = 1,
        StartTime = DateTime.Now.AddHours(2),
        Movie = movie,
        Tickets = new List<Ticket>(),
        Capacity = 100,
        TicketsAvailable = 100
    };
    movie.Showtimes.Add(showtime); // Add the showtime to the movie's showtimes list

    var ticket = new Ticket
    {
        Id = 1,
        Price = 15.00m,
        Showtime = showtime
    };

    _mockService.Setup(x => x.EditTicket(1, ticket.Price)).ReturnsAsync(false);

    // Act
    var result = await _controller.EditTicketPrice(1, new EditTicketPriceDto { NewPrice = ticket.Price });

    // Assert
    var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
    _mockService.Verify(x => x.EditTicket(1, ticket.Price), Times.Once);
}

    }
}
