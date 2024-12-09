public class CartDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who owns the cart.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the movie associated with the cart.
    /// </summary>
    public int MovieId { get; set; }

    /// <summary>
    /// Gets or sets the list of tickets in the cart.
    /// </summary>
    public List<CartTicketDto> Tickets { get; set; } = new List<CartTicketDto>();

    /// <summary>
    /// Gets the total price of the tickets in the cart.
    /// Calculates the total price by summing the price multiplied by the quantity for each ticket.
    /// </summary>
    public decimal TotalPrice => Tickets.Sum(t => t.Price * t.Quantity);
}
