public class OrderReceiptDto
{
    // The unique ID of the order.
    public int OrderId { get; set; }

    // The date and time when the order was processed.
    public DateTime ProcessedDate { get; set; }

    // The total price of the order, including all tickets.
    public decimal TotalPrice { get; set; }

    // A list of tickets included in the order.
    public List<OrderTicketDto> Tickets { get; set; }
}
