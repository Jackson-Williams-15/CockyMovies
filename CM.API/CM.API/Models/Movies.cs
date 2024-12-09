namespace CM.API.Models;
public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateReleased { get; set; }
    public List<Genre>? Genres { get; set; }
    public required List<Showtime> Showtimes { get; set; }
    public List<Review>? Reviews { get; set; }
    // Set default image URL
    private string _imageUrl = @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MT
    l8fG1vdmllfGVufDB8fDB8fHww";
    public string ImageUrl
    {
        get => string.IsNullOrWhiteSpace(_imageUrl) ? @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxM
        jA3fDB8MHxzZWFyY2h8MTl8fG1vdmllfGVufDB8fDB8fHww" : _imageUrl;
        set => _imageUrl = value ?? @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2
        h8MTl8fG1vdmllfGVufDB8fDB8fHww";
    }

    public int RatingId { get; set; }
    public Rating Rating { get; set; }
}