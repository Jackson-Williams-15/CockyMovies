using CM.API.Models;

public class CartTicketDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public ShowtimeDto Showtime { get; set; }
    public Movie Movie { get; set; }
    public int MovieId { get; set; }
    public int Quantity { get; set; }
    public bool IsSold { get; set; }
    public int ShowtimeId { get; set; }

}