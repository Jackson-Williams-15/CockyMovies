public class CartDto
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public List<CartTicketDto> Tickets { get; set; } = new List<CartTicketDto>();
    public decimal TotalPrice => Tickets.Sum(t => t.Price * t.Quantity);
}