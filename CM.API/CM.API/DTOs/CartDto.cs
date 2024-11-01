public class CartDto
{
    public int UserId { get; set; }
    public List<TicketDto> Tickets { get; set; }
    public DateTime UpdatedAt { get; set; }
}