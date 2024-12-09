public class OrderReceiptDto
{
    public int OrderId { get; set; }
    public DateTime ProcessedDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderTicketDto>? Tickets { get; set; }
}