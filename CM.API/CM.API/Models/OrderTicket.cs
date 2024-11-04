namespace CM.API.Models;
public class OrderTicket
{
    public int OrderTicketId { get; set; }
    public int TicketId { get; set; }
    public int ShowtimeId { get; set; }
    public decimal Price { get; set; }
    public Showtime Showtime { get; set; }
    public Movie Movie { get; set; }
}