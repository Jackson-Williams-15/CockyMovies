namespace CM.API.Models;

using System.ComponentModel.DataAnnotations.Schema;


public class Ticket
{
    public int Id { get; set; }
    public decimal Price { get; set; }

    // Foreign key for the associated showtime
    public int ShowtimeId { get; set; }
    public required Showtime Showtime { get; set; }

    [ForeignKey("Cart")]
    public int? CartId { get; set; }  // Nullable to allow Tickets to exist without being in a Cart
    public Cart Cart { get; set; }
}