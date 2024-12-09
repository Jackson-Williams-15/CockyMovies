using CM.API.Models;

public class CartTicketDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the ticket.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticket.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the showtime associated with this ticket.
    /// This property is optional and may be null if no showtime is set.
    /// </summary>
    public ShowtimeDto? Showtime { get; set; }

    /// <summary>
    /// Gets or sets the movie associated with the ticket.
    /// This property is optional and may be null if no movie is set.
    /// </summary>
    public Movie? Movie { get; set; }

    /// <summary>
    /// Gets or sets the ID of the movie associated with this ticket.
    /// </summary>
    public int MovieId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of tickets of this type in the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets whether the ticket has been sold.
    /// </summary>
    public bool IsSold { get; set; }

    /// <summary>
    /// Gets or sets the ID of the showtime associated with this ticket.
    /// </summary>
    public int ShowtimeId { get; set; }
}
