public class ShowtimeDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public List<TicketDto> Tickets { get; set; }
}