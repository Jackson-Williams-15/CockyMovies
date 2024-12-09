namespace CM.API.Models
{
    /// <summary>
    /// Represents a ticket associated with an order.
    /// </summary>
    public class OrderTicket
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order ticket.
        /// This is the primary key for the OrderTicket entity.
        /// </summary>
        public int OrderTicketId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the ticket associated with this order ticket.
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the showtime associated with this order ticket.
        /// </summary>
        public int ShowtimeId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the movie associated with this order ticket.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the price of this specific ticket in the order.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the showtime details for the ticket.
        /// This is a navigation property to the Showtime entity.
        /// </summary>
        public Showtime? Showtime { get; set; }

        /// <summary>
        /// Gets or sets the movie details for the ticket.
        /// This is a navigation property to the Movie entity.
        /// </summary>
        public Movie? Movie { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated order result.
        /// This is a foreign key linking this ticket to the OrderResult entity.
        /// </summary>
        public int OrderResultId { get; set; }

        /// <summary>
        /// Gets or sets the order result associated with this order ticket.
        /// This is a navigation property to the OrderResult entity.
        /// </summary>
        public OrderResult? OrderResult { get; set; }

        /// <summary>
        /// Gets or sets
