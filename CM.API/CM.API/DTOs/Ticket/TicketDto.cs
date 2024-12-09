/// <summary>
/// Data transfer object for a ticket.
/// </summary>
public class TicketDto
{
    /// <summary>
    /// Gets or sets the ticket ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticket.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated showtime.
    /// </summary>
    public int ShowtimeId { get; set; } // Foward thinking
}