public class OrderTicketDto
{
    // The unique ID of the ticket.
    public int TicketId { get; set; }

    // The unique ID of the showtime.
    public int ShowtimeId { get; set; }

    // The title of the movie or event.
    public string MovieTitle { get; set; }

    // The start time of the showtime.
    public DateTime ShowtimeStartTime { get; set; }

    // The price of the ticket.
    public decimal Price { get; set; }

    // The number of tickets being ordered.
    public int Quantity { get; set; }
}
