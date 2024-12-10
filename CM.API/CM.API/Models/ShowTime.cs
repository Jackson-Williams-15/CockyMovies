using System.ComponentModel.DataAnnotations;

namespace CM.API.Models;

/// <summary>
/// Represents a showtime for a movie.
/// </summary>
public class Showtime
{
    /// <summary>
    /// Gets or sets the unique identifier for the showtime.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the start time of the showtime.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated movie.
    /// </summary>
    public int MovieId { get; set; }

    /// <summary>
    /// Gets or sets the associated movie.
    /// </summary>
    public Movie? Movie { get; set; }

    /// <summary>
    /// Gets or sets the collection of tickets for the showtime.
    /// </summary>
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    /// <summary>
    /// Gets or sets the maximum number of seats available for the showtime.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Gets the number of tickets sold for the showtime.
    /// </summary>
    public int TicketsSold => Tickets.Count;

    /// <summary>
    /// Gets or sets the number of tickets available for the showtime.
    /// </summary>
    public int TicketsAvailable { get; set; }
}
