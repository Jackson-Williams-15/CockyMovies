namespace CM.API.Models;

/// <summary>
/// Represents a ticket for a showtime.
/// </summary>
public class Ticket
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
    public int ShowtimeId { get; set; }

    /// <summary>
    /// Gets or sets the associated showtime.
    /// </summary>
    public Showtime? Showtime { get; set; }

    /// <summary>
    /// Gets or sets the quantity of tickets.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ticket is sold.
    /// </summary>
    public bool IsSold { get; set; }

}