namespace CM.API.Models;

public class Ticket
{
    public int Id { get; set; }
    public decimal Price { get; set; }

    // Foreign key for the associated showtime
    public int ShowtimeId { get; set; }
    public required Showtime Showtime { get; set; }
    public int Quantity { get; set; }
    public bool IsSold { get; set; }

}