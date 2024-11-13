using System.ComponentModel.DataAnnotations;
    public class ReviewCreateDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string? Title { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // Rating out of 5

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }

        [Required]
        public int MovieId { get; set; }
    }