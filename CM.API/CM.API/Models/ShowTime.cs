using System.ComponentModel.DataAnnotations;  // Data annotations for validation

namespace CM.API.Models
{
    public class Showtime
    {
        // Unique identifier for the showtime
        public int Id { get; set; }

        // The start time of the showtime
        public DateTime StartTime { get; set; }

        // Foreign key linking to the associated movie
        public int MovieId { get; set; }

        // Navigation property for the associated Movie
        public required Movie Movie { get; set; }

        // Collection of tickets available for this showtime
        public required ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        // Maximum number of seats available for this showtime
        public int Capacity { get; set; }

        // Number of tickets already sold for this showtime
        public int TicketsSold => Tickets.Count;

        // Number of tickets available for purchase
        public int TicketsAvailable { get; set; }
    }
}
