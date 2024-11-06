using System.ComponentModel.DataAnnotations;

namespace CM.API.Models;
public class Showtime
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }

    // Foreign key for the associated movie
    public int MovieId { get; set; }

    public required Movie Movie { get; set; }

    public required ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    // Max number of seats available
    public int Capacity { get; set; }
    public int TicketsSold => Tickets.Count;
    public int TicketsAvailable { get; set; }
}
