namespace CM.API.Models;
    public class Reply
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string? Author { get; set; }
        public string? Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Review? Review { get; set; } // Navigation property
    }