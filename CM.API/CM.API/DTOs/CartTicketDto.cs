public class CartTicketDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public ShowtimeDto Showtime { get; set; }
    public int Quantity { get; set; }
    public bool IsSold { get; set; }
    public int ShowtimeId { get; set; }

}