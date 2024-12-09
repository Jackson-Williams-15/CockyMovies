namespace CM.API.Models
{
    /// <summary>
    /// Represents a ticket associated with a specific showtime.
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Gets or sets the ID of the ticket.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the price of the ticket.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated showtime.
        /// </summary>
        public int ShowtimeId { get; set; }

        /// <summary>
        /// Gets or sets the associated showtime for the ticket.
        /// </summary>
        public Showtime? Showtime { get; set; }

        /// <summary>
        /// Gets or sets the quantity of tickets available.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ticket has been sold.
        /// </summary>
        public bool IsSold { get; set; }
    }
}
