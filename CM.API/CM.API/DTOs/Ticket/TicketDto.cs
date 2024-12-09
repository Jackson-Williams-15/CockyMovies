public class TicketDto
{
    // Unique identifier for the ticket.
    public int Id { get; set; }

    // The price of the ticket.
    public decimal Price { get; set; }

    // The ID of the Showtime associated with this ticket.
    public int ShowtimeId { get; set; }
}
