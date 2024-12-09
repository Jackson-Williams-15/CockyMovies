public class OrderTicketDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the ticket.
    /// </summary>
    public int TicketId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the showtime associated with the ticket.
    /// </summary>
    public int ShowtimeId { get; set; }

    /// <summary>
    /// Gets or sets the title of the movie associated with the ticket.
    /// </summary>
    public string? MovieTitle { get; set; }

    /// <summary>
    /// Gets or sets the start time of the showtime for the ticket.
    /// </summary>
    public DateTime ShowtimeStartTime { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticket.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the quantity of tickets of this type in the order.
    /// </summary>
    public int Quantity { get; set; }
}
