public class OrderTicketDto
{
    public int TicketId { get; set; }
    public int ShowtimeId { get; set; }
    public string MovieTitle { get; set; }
    public DateTime ShowtimeStartTime { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}