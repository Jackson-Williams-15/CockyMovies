namespace CM.API.Models
{
    /// <summary>
    /// Represents an order for a ticket associated with a specific showtime and movie.
    /// </summary>
    public class OrderTicket
    {
        /// <summary>
        /// Gets or sets the unique ID of the order ticket.
        /// </summary>
        public int OrderTicketId { get; set; }

        /// <summary>
        /// Gets or sets the ticket ID associated with this order.
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// Gets or sets the showtime ID associated with the ticket order.
        /// </summary>
        public int ShowtimeId { get; set; }

        /// <summary>
        /// Gets or sets the movie ID associated with the ticket order.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the price of the ticket for this order.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the associated showtime for this ticket order.
        /// </summary>
        public Showtime? Showtime { get; set; }

        /// <summary>
        /// Gets or sets the associated movie for this ticket order.
        /// </summary>
        public Movie? Movie { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the associated order result.
        /// </summary>
        public int OrderResultId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated order result.
        /// </summary>
        public OrderResult? OrderResult { get; set; }

        /// <summary>
        /// Gets or sets the quantity of tickets ordered.
        /// </summary>
        public int Quantity { get; set; }
    }
}
