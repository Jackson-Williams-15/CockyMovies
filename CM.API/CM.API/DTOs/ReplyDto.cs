public class ReplyDto
{
    public int Id { get; set; }
    public int ReviewId { get; set; }
    public string? Author { get; set; }
    public string? Body { get; set; }
    public DateTime CreatedAt { get; set; }
}