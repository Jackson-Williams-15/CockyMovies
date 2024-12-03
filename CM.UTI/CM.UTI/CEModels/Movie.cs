using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? DateReleased { get; set; }

    public int? CartId { get; set; }

    public int? GenresId { get; set; }

    public int? ShowtimesId { get; set; }

    public int? ReviewsId { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageUrl1 { get; set; }

    public int? RatingId { get; set; }
}
