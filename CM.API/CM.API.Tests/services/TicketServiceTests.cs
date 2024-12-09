using CM.API.Data;
using CM.API.Models;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
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
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB instance
            .Options;

        _context = new AppDbContext(options);
        _ticketService = new TicketService(_context);
    }

    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    private (int MovieId, int ShowtimeId, int TicketId) SeedDatabase()
    {
        var movieId = Guid.NewGuid().GetHashCode();
        var showtimeId = Guid.NewGuid().GetHashCode();
        var ticketId = Guid.NewGuid().GetHashCode();

        var movie = new Movie
        {
            Id = movieId,
            Title = "Test Movie",
            DateReleased = DateTime.Now,
            RatingId = Guid.NewGuid().GetHashCode()
        };

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddDays(1),
            Capacity = 100,
            TicketsAvailable = 90,
            MovieId = movieId,
            Movie = movie
        };

        var ticket = new Ticket
        {
            Id = ticketId,
            Price = 10.00m,
            ShowtimeId = showtimeId,
            Showtime = showtime
        };

        _context.Movies.Add(movie);
        _context.Showtime.Add(showtime);
        _context.Ticket.Add(ticket);
        _context.SaveChanges();

        return (movieId, showtimeId, ticketId);
    }

    // --- Restored Tests ---

    [Fact]
    public async Task AddTicket_ShouldAddTicket_WhenValid()
    {
        ResetDatabase();
        var (_, showtimeId, _) = SeedDatabase();

        var newTicketId = Guid.NewGuid().GetHashCode();
        var ticket = new Ticket
        {
            Id = newTicketId,
            Price = 15.00m,
            ShowtimeId = showtimeId
        };

        var result = await _ticketService.AddTicket(ticket);
        Assert.True(result);
        Assert.NotNull(await _context.Ticket.FindAsync(newTicketId));
    }

    [Fact]
    public async Task AddTicket_ShouldFail_WhenTicketAlreadyExists()
    {
        ResetDatabase();
        var (_, showtimeId, ticketId) = SeedDatabase();

        var ticket = new Ticket
        {
            Id = ticketId,
            Price = 10.00m,
            ShowtimeId = showtimeId
        };

        var result = await _ticketService.AddTicket(ticket);
        Assert.False(result);
    }

    [Fact]
    public async Task EditTicket_ShouldFail_WhenTicketNotFound()
    {
        ResetDatabase();

        var result = await _ticketService.EditTicket(-1, 20.00m);
        Assert.False(result);
    }

    [Fact]
    public async Task GetTicketById_ShouldReturnTicket_WhenExists()
    {
        ResetDatabase();
        var (_, _, ticketId) = SeedDatabase();

        var ticket = await _ticketService.GetTicketById(ticketId);
        Assert.NotNull(ticket);
        Assert.Equal(ticketId, ticket.Id);
    }

    [Fact]
    public async Task GetTicketById_ShouldReturnNull_WhenNotFound()
    {
        ResetDatabase();

        var ticket = await _ticketService.GetTicketById(-1);
        Assert.Null(ticket);
    }


    [Fact]
    public async Task RemoveTicketsFromShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        ResetDatabase();

        var result = await _ticketService.RemoveTicketsFromShowtime(-1, 1);
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketsToShowtime_ShouldAddTickets_WhenValid()
    {
        ResetDatabase();
        var (_, showtimeId, _) = SeedDatabase();

        var result = await _ticketService.AddTicketsToShowtime(showtimeId, 10);
        Assert.True(result);

        var tickets = await _context.Ticket.Where(t => t.ShowtimeId == showtimeId).ToListAsync();
        Assert.Equal(11, tickets.Count); // 1 original + 10 new
    }

    [Fact]
    public async Task AddTicketsToShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        ResetDatabase();

        var result = await _ticketService.AddTicketsToShowtime(-1, 10);
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketsToShowtime_ShouldFail_WhenExceedsCapacity()
    {
        ResetDatabase();
        var (_, showtimeId, _) = SeedDatabase();

        var result = await _ticketService.AddTicketsToShowtime(showtimeId, 200); // Exceeds available capacity
        Assert.False(result);
    }
}
