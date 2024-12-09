namespace CM.API.Models;

public class Ticket
{
    // The unique identifier for the ticket
    public int Id { get; set; }
    
    // The price of the ticket
    public decimal Price { get; set; }

    // Foreign key linking the ticket to a specific showtime
    public int ShowtimeId { get; set; }

    // Navigation property to the related Showtime
    public required Showtime Showtime { get; set; }

    // Quantity of tickets available
    public int Quantity { get; set; }

    // Boolean flag indicating whether the ticket has been sold
    public bool IsSold { get; set; }
}
