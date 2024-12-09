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
        private readonly Mock<ITicketService> _mockService; // Mock for ITicketService
        private readonly TicketsController _controller; // The controller under test

        // Constructor to set up mock service and controller instance
        public TicketControllerTests()
        {
            _mockService = new Mock<ITicketService>(); // Initialize the mock for ITicketService
            _controller = new TicketsController(_mockService.Object); // Initialize the controller with the mocked service
        }

        // Test for AddTicket when the ticket is successfully added
        [Fact]
        public async Task AddTicket_ShouldReturnOk_WhenTicketIsAdded()
        {
            // Arrange: Set up test data
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

            _mockService.Setup(x => x.AddTicket(ticket)).ReturnsAsync(true); // Mock AddTicket to return true

            // Act: Call the controller method
            var result = await _controller.AddTicket(ticket);

            // Assert: Verify that the result is CreatedAtActionResult and the AddTicket method was called
            var createdResult = Assert.IsType<CreatedAtActionResult>(result); // Expect Created result
            _mockService.Verify(x => x.AddTicket(ticket), Times.Once); // Verify AddTicket was called exactly once
        }

        // Test for AddTicket when the ticket cannot be added
        [Fact]
        public async Task AddTicket_ShouldReturnBadRequest_WhenTicketCannotBeAdded()
        {
            // Arrange: Set up test data for failed addition
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
            movie.Showtimes.Add(showtime);

            var ticket = new Ticket
            {
                Id = 1,
                Price = 10.00m,
                Showtime = showtime
            };

            _mockService.Setup(x => x.AddTicket(ticket)).ReturnsAsync(false); // Mock AddTicket to return false

            // Act: Call the controller method
            var result = await _controller.AddTicket(ticket);

            // Assert: Verify that the result is BadRequestObjectResult and the AddTicket method was called
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Expect BadRequest result
            _mockService.Verify(x => x.AddTicket(ticket), Times.Once); // Verify AddTicket was called exactly once
        }

        // Test for EditTicket when the ticket is successfully updated
        [Fact]
        public async Task EditTicket_ShouldReturnOk_WhenTicketIsUpdated()
        {
            // Arrange: Set up test data for ticket update
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
            movie.Showtimes.Add(showtime);

            var ticket = new Ticket
            {
                Id = 1,
                Price = 15.00m,
                Showtime = showtime
            };

            _mockService.Setup(x => x.EditTicket(1, ticket.Price)).ReturnsAsync(true); // Mock EditTicket to return true

            // Act: Call the controller method to edit ticket price
            var result = await _controller.EditTicketPrice(1, new EditTicketPriceDto { NewPrice = ticket.Price });

            // Assert: Verify the result is OkObjectResult and the EditTicket method was called
            var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
            _mockService.Verify(x => x.EditTicket(1, ticket.Price), Times.Once); // Verify EditTicket was called exactly once
        }

        // Test for EditTicket when ticket update fails
        [Fact]
        public async Task EditTicket_ShouldReturnNotFound_WhenTicketUpdateFails()
        {
            // Arrange: Set up test data for failed ticket update
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
            movie.Showtimes.Add(showtime);

            var ticket = new Ticket
            {
                Id = 1,
                Price = 15.00m,
                Showtime = showtime
            };

            _mockService.Setup(x => x.EditTicket(1, ticket.Price)).ReturnsAsync(false); // Mock EditTicket to return false

            // Act: Call the controller method to edit ticket price
            var result = await _controller.EditTicketPrice(1, new EditTicketPriceDto { NewPrice = ticket.Price });

            // Assert: Verify the result is NotFoundObjectResult and the EditTicket method was called
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result); // Expect NotFound result
            _mockService.Verify(x => x.EditTicket(1, ticket.Price), Times.Once); // Verify EditTicket was called exactly once
        }
    }
}
