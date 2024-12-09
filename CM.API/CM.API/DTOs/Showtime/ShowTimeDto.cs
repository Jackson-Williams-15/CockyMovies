public class ShowtimeDto
{
    // Unique identifier for the showtime
    public int Id { get; set; }

    // Start time of the showtime
    public DateTime StartTime { get; set; }

    // List of tickets associated with the showtime
    public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();

    // Number of available tickets for the showtime
    public int AvailableTickets { get; set; }

    // Price of a ticket
    public decimal TicketPrice => Tickets.FirstOrDefault()?.Price ?? 0;

    // Movie details related to the showtime
    public MovieDto? Movie { get; set; }
    public int Capacity { get; set; }
}
