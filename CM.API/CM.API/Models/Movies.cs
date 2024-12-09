namespace CM.API.Models
{
    // Represents a movie entity.
    public class Movie
    {
        // The unique identifier for the movie.
        public int Id { get; set; }

        // The title of the movie. This is a required field.
        public required string Title { get; set; }

        // A brief description of the movie, optional.
        public string? Description { get; set; }

        // The release date of the movie.
        public DateTime DateReleased { get; set; }

        // A list of genres associated with the movie. This is optional, meaning a movie may not have genres defined.
        public List<Genre>? Genres { get; set; }

        // A list of showtimes for the movie. This is a required property, as every movie must have at least one associated showtime.
        public required List<Showtime> Showtimes { get; set; }

        // A list of reviews associated with the movie. This is optional, as not all movies will have reviews.
        public List<Review>? Reviews { get; set; }

        // Private field to store the movie's image URL. It has a default value.
        private string _imageUrl = @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fG1vdmllfGVufDB8fDB8fHww";

        // Property to get and set the image URL of the movie.
        public string ImageUrl
        {
            // Returns the default image URL if _imageUrl is null or white space.
            get => string.IsNullOrWhiteSpace(_imageUrl) 
                ? @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fG1vdmllfGVufDB8fDB8fHww" 
                : _imageUrl;

            // If the provided value is null, sets the default image URL, otherwise sets the given value.
            set => _imageUrl = value ?? @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fG1vdmllfGVufDB8fDB8fHww";
        }

        // The rating ID associated with the movie.
        public int RatingId { get; set; }

        // The Rating object that provides additional details about the movie's rating.
        public Rating Rating { get; set; }
    }
}
