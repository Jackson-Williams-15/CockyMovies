/// <summary>
/// Data Transfer Object (DTO) for representing a ticket.
/// </summary>
public class TicketDto
{
    /// <summary>
    /// Gets or sets the ID of the ticket.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticket.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the ID of the associated showtime for the ticket.
    /// </summary>
    public int ShowtimeId { get; set; }
}
