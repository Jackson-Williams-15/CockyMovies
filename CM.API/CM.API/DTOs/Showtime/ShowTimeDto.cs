/// <summary>
/// Gets or sets the unique identifier for the showtime.
/// </summary>
public class ShowtimeDto
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
    /// Gets or sets the collection of tickets for the showtime.
    /// </summary>
    public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();

    /// <summary>
    /// Gets or sets the number of available tickets for the showtime.
    /// </summary>
    public int AvailableTickets { get; set; }

    /// <summary>
    /// Gets the price of a ticket for the showtime.
    /// </summary>
    public decimal TicketPrice => Tickets.FirstOrDefault()?.Price ?? 0;

    /// <summary>
    /// Gets or sets the associated movie.
    /// </summary>
    public MovieDto? Movie { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of seats available for the showtime.
    /// </summary>
    public int Capacity { get; set; }
}