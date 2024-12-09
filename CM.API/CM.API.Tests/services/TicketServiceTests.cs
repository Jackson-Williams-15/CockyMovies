using CM.API.Data;
using CM.API.Models;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class TicketServiceTests
{
    private readonly TicketService _ticketService;
    private readonly AppDbContext _context;

    public TicketServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _ticketService = new TicketService(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var movie = new Movie
        {
            Id = 1,
            Title = "Test Movie",
            DateReleased = DateTime.Now,
            RatingId = 1,
            Showtimes = new List<Showtime>()
        };

        var showtime = new Showtime
        {
            Id = 1,
            StartTime = DateTime.Now.AddDays(1),
            Capacity = 100,
            TicketsAvailable = 90,
            MovieId = 1,
            Movie = movie,
            Tickets = new List<Ticket>()
        };

        var ticket = new Ticket
        {
            Id = 1,
            Price = 10.00m,
            ShowtimeId = 1,
            Showtime = showtime
        };

        movie.Showtimes.Add(showtime);
        showtime.Tickets.Add(ticket);

        _context.Movies.Add(movie);
        _context.Showtime.Add(showtime);
        _context.Ticket.Add(ticket);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddTicket_ShouldAddTicket_WhenValid()
    {
        // Arrange
        var showtime = await _context.Showtime.FirstAsync();
        var ticket = new Ticket
        {
            Id = 2,
            Price = 15.00m,
            ShowtimeId = showtime.Id,
            Showtime = showtime // Required property set here
        };

        // Act
        var result = await _ticketService.AddTicket(ticket);

        // Assert
        Assert.True(result);
        Assert.NotNull(await _context.Ticket.FindAsync(2));
    }

    [Fact]
    public async Task AddTicket_ShouldFail_WhenTicketAlreadyExists()
    {
        // Arrange
        var showtime = await _context.Showtime.FirstAsync();
        var ticket = new Ticket
        {
            Id = 1, 
            Price = 10.00m, 
            ShowtimeId = showtime.Id, 
            Showtime = showtime // Required property set here
        };

        // Act
        var result = await _ticketService.AddTicket(ticket);

        // Assert
        Assert.False(result);
}


    [Fact]
    public async Task EditTicket_ShouldUpdateTicketPrice_WhenValid()
    {
        // Act
        var result = await _ticketService.EditTicket(1, 20.00m);

        // Assert
        Assert.True(result);
        var updatedTicket = await _context.Ticket.FindAsync(1);
        Assert.Equal(20.00m, updatedTicket.Price);
    }

    [Fact]
    public async Task EditTicket_ShouldFail_WhenShowtimeNotFound()
    {
        // Act
        var result = await _ticketService.EditTicket(999, 20.00m);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetAllTickets_ShouldReturnTickets_WhenTheyExist()
    {
        // Act
        var tickets = await _ticketService.GetAllTickets();

        // Assert
        Assert.NotEmpty(tickets);
        Assert.Equal(1, tickets.Count);
    }

    [Fact]
    public async Task GetTicketById_ShouldReturnTicket_WhenExists()
    {
        // Act
        var ticket = await _ticketService.GetTicketById(1);

        // Assert
        Assert.NotNull(ticket);
        Assert.Equal(1, ticket.Id);
    }

    [Fact]
    public async Task GetTicketById_ShouldReturnNull_WhenNotFound()
    {
        // Act
        var ticket = await _ticketService.GetTicketById(999);

        // Assert
        Assert.Null(ticket);
    }

    [Fact]
    public async Task RemoveTicketsFromShowtime_ShouldRemoveTickets_WhenValid()
    {
        // Act
        var result = await _ticketService.RemoveTicketsFromShowtime(1, 1);

        // Assert
        Assert.True(result);
        var remainingTickets = await _context.Ticket.CountAsync();
        Assert.Equal(0, remainingTickets);
    }

    [Fact]
    public async Task RemoveTicketsFromShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        // Act
        var result = await _ticketService.RemoveTicketsFromShowtime(999, 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketsToShowtime_ShouldAddTickets_WhenValid()
    {
        // Act
        var result = await _ticketService.AddTicketsToShowtime(1, 10);

        // Assert
        Assert.True(result);
        var tickets = await _context.Ticket.Where(t => t.ShowtimeId == 1).ToListAsync();
        Assert.Equal(11, tickets.Count); // 1 original + 10 new
    }

    [Fact]
    public async Task AddTicketsToShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        // Act
        var result = await _ticketService.AddTicketsToShowtime(999, 10);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketsToShowtime_ShouldFail_WhenExceedsCapacity()
    {
        // Act
        var result = await _ticketService.AddTicketsToShowtime(1, 100);

        // Assert
        Assert.False(result);
    }
}
