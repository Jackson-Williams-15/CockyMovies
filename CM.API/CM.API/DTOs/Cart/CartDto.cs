public class CartDto
{
    // The unique ID of the cart.
    public int CartId { get; set; }

    // The unique ID of the user who owns the cart.
    public int UserId { get; set; }

    // The ID of the movie associated with the cart.
    public int MovieId { get; set; }

    // A list of tickets in the cart.
    // Each ticket is represented by a CartTicketDto.
    public List<CartTicketDto> Tickets { get; set; } = new List<CartTicketDto>();

    // The total price of all tickets in the cart.
    // It multiplies the price of each ticket by its quantity and sums the results.
    public decimal TotalPrice => Tickets.Sum(t => t.Price * t.Quantity);
}
