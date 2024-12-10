using Microsoft.EntityFrameworkCore;
using CM.API.Models;

namespace CM.API.Data
{
    /// <summary>
    /// Contains extension methods for the ModelBuilder class.
    /// </summary>
    public static class ModelBuilderExtensions
    {   
        /// <summary>
        /// Seeds the database with initial data.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance.</param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Hash the password for the manager account
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("managerpassword");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    Username = "Manager",
                    Email = "managerEmail@example.com",
                    Password = hashedPassword, // Use the hashed password
                    Role = "Manager"
                }
            );

            // Data seed ratings
            modelBuilder.Entity<Rating>().HasData(
                new Rating { Id = 1, Name = "G" },
                new Rating { Id = 2, Name = "PG" },
                new Rating { Id = 3, Name = "PG-13" },
                new Rating { Id = 4, Name = "R" },
                new Rating { Id = 5, Name = "NC-17" }
            );

            // Data seed genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Horror" },
                new Genre { Id = 5, Name = "Sci-Fi" },
                new Genre { Id = 6, Name = "Romance" },
                new Genre { Id = 7, Name = "Thriller" },
                new Genre { Id = 8, Name = "Fantasy" },
                new Genre { Id = 9, Name = "Documentary" },
                new Genre { Id = 10, Name = "Animation" }
            );


            // Data seed movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 11,
                    Title = "Inception",
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology.",
                    DateReleased = new DateTime(2010, 7, 16),
                    RatingId = 3,
                    ImageUrl = "https://static1.srcdn.com/wordpress/wp-content/uploads/2019/12/Inception-movie-poster-1.jpg"
                },
                new Movie
                {
                    Id = 12,
                    Title = "The Matrix",
                    Description = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.",
                    DateReleased = new DateTime(1999, 3, 31),
                    RatingId = 4,
                    ImageUrl = "https://www.syfy.com/sites/syfy/files/2021/03/the-matrix.jpeg"
                },
                new Movie
                {
                    Id = 13,
                    Title = "The Godfather",
                    Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    DateReleased = new DateTime(1972, 3, 24),
                    RatingId = 4,
                    ImageUrl = "https://ntvb.tmsimg.com/assets/p6326_v_h8_be.jpg?w=1280&h=720"
                },
                new Movie
                {
                    Id = 14,
                    Title = "The Dark Knight",
                    Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                    DateReleased = new DateTime(2008, 7, 18),
                    RatingId = 4,
                    ImageUrl = "https://theconsultingdetectivesblog.com/wp-content/uploads/2014/06/the-dark-knight-original.jpg"
                },
                new Movie
                {
                    Id = 15,
                    Title = "Pulp Fiction",
                    Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    DateReleased = new DateTime(1994, 10, 14),
                    RatingId = 4,
                    ImageUrl = "https://waterfire.org/wp-content/uploads/2020/12/maxresdefault-5-1024x576.jpg"
                },
                new Movie
                {
                    Id = 16,
                    Title = "Forrest Gump",
                    Description = "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold from the perspective of an Alabama man with an IQ of 75.",
                    DateReleased = new DateTime(1994, 7, 6),
                    RatingId = 2,
                    ImageUrl = "https://you.stonybrook.edu/mysocproject/files/2020/03/Forrest-Gump.jpg"
                }
            );

            // Data seed showtimes
            modelBuilder.Entity<Showtime>().HasData(
                new Showtime
                {
                    Id = 1,
                    StartTime = new DateTime(2024, 12, 14, 11, 0, 0),
                    MovieId = 11,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 2,
                    StartTime = new DateTime(2024, 12, 14, 11, 30, 0),
                    MovieId = 11,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 3,
                    StartTime = new DateTime(2024, 12, 15, 14, 0, 0),
                    MovieId = 12,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 4,
                    StartTime = new DateTime(2024, 12, 15, 14, 15, 0),
                    MovieId = 12,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 5,
                    StartTime = new DateTime(2024, 12, 16, 16, 0, 0),
                    MovieId = 13,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 6,
                    StartTime = new DateTime(2024, 12, 16, 16, 30, 0),
                    MovieId = 13,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 7,
                    StartTime = new DateTime(2024, 12, 18, 10, 15, 0),
                    MovieId = 14,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 8,
                    StartTime = new DateTime(2023, 12, 18, 11, 0, 0),
                    MovieId = 14,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 9,
                    StartTime = new DateTime(2023, 12, 16, 17, 0, 0),
                    MovieId = 15,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 10,
                    StartTime = new DateTime(2023, 12, 16, 17, 30, 0),
                    MovieId = 15,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 11,
                    StartTime = new DateTime(2023, 12, 26, 14, 0, 0),
                    MovieId = 16,
                    Capacity = 25,
                    TicketsAvailable = 25
                },
                new Showtime
                {
                    Id = 12,
                    StartTime = new DateTime(2023, 12, 26, 17, 0, 0),
                    MovieId = 16,
                    Capacity = 25,
                    TicketsAvailable = 25
                }
            );

            // Data seed tickets
            var tickets = new List<Ticket>();
            int ticketId = 1;
            for (int showtimeId = 1; showtimeId <= 12; showtimeId++)
            {
                for (int i = 0; i < 25; i++)
                {
                    tickets.Add(new Ticket { Id = ticketId++, ShowtimeId = showtimeId, Price = 10.00m });
                }
            }
            modelBuilder.Entity<Ticket>().HasData(tickets);

            // Data seed movie genres
            modelBuilder.Entity("MovieGenres").HasData(
                new { MoviesId = 11, GenresId = 1 }, // Inception - Action
                new { MoviesId = 11, GenresId = 5 }, // Inception - Sci-Fi
                new { MoviesId = 12, GenresId = 1 }, // The Matrix - Action
                new { MoviesId = 12, GenresId = 5 }, // The Matrix - Sci-Fi
                new { MoviesId = 13, GenresId = 3 }, // The Godfather - Drama
                new { MoviesId = 14, GenresId = 1 }, // The Dark Knight - Action
                new { MoviesId = 14, GenresId = 7 }, // The Dark Knight - Thriller
                new { MoviesId = 15, GenresId = 3 }, // Pulp Fiction - Drama
                new { MoviesId = 15, GenresId = 7 }, // Pulp Fiction - Thriller
                new { MoviesId = 16, GenresId = 3 }, // Forrest Gump - Drama
                new { MoviesId = 16, GenresId = 6 }  // Forrest Gump - Romance
            );
        }
    }
}