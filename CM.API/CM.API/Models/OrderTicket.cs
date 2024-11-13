namespace CM.API.Models;
public class OrderTicket
{
    public int OrderTicketId { get; set; }
    public int TicketId { get; set; }
    public int ShowtimeId { get; set; }
    public int MovieId { get; set; }
    public decimal Price { get; set; }
    public Showtime Showtime { get; set; }
    public Movie Movie { get; set; }
    public int OrderResultId { get; set; } // Foreign key to OrderResult
    public OrderResult OrderResult { get; set; } // Navigation property
    public int Quantity { get; set; }
}