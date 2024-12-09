public class OrderReceiptDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was processed.
    /// </summary>
    public DateTime ProcessedDate { get; set; }

    /// <summary>
    /// Gets or sets the total price of the order.
    /// This includes the total price of all tickets in the order.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets or sets the list of tickets associated with the order.
    /// This provides details about each ticket purchased in the order.
    /// </summary>
    public List<OrderTicketDto>? Tickets { get; set; }
}
