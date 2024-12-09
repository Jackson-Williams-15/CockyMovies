using CM.API.Models;

public class CartTicketDto
{
    // The unique ID of the cart ticket.
    public int Id { get; set; }

    // The price of the ticket.
    public decimal Price { get; set; }

    // The showtime details for the ticket (optional).
    public ShowtimeDto? Showtime { get; set; }

    // The movie associated with the ticket (optional).
    public Movie? Movie { get; set; }

    // The ID of the movie associated with the ticket.
    public int MovieId { get; set; }

    // The quantity of tickets for this movie/showtime.
    public int Quantity { get; set; }

    // Indicates if the ticket has been sold.
    public bool IsSold { get; set; }

    // The ID of the showtime for this ticket.
    public int ShowtimeId { get; set; }
}
