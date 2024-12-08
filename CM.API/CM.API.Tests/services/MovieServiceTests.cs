using Moq;
using Xunit;
using CM.API.Models;
using CM.API.Data;
using CM.API.Repositories;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CM.API.Tests
{
    public class MovieServiceTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<GenreRepository> _mockGenreRepository;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockGenreRepository = new Mock<GenreRepository>(_mockContext.Object);
            _movieService = new MovieService(_mockContext.Object, _mockGenreRepository.Object);
        }

        [Fact]
        public async Task AddMovie_MovieAlreadyExists_ReturnsFalse()
        {
            // Arrange
            var movie = new Movie { Id = 1, Name = "Test Movie" };
            var movieList = new List<Movie> { movie }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Movie>>();
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(movieList.Provider);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(movieList.Expression);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(movieList.ElementType);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(movieList.GetEnumerator());

            _mockContext.Setup(c => c.Movies).Returns(mockDbSet.Object);

            // Act
            var result = await _movieService.AddMovie(movie);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddMovie_ValidMovie_AddsMovieAndReturnsTrue()
        {
            // Arrange
            var movie = new Movie { Id = 2, Name = "New Movie" };
            var mockDbSet = new Mock<DbSet<Movie>>();
            _mockContext.Setup(c => c.Movies).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _movieService.AddMovie(movie);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveMovie_MovieNotFound_ReturnsFalse()
        {
            // Arrange
            var movie = new Movie { Id = 3, Name = "Nonexistent Movie" };
            _mockContext.Setup(c => c.Movies.FindAsync(movie.Id)).ReturnsAsync((Movie)null);

            // Act
            var result = await _movieService.RemoveMovie(movie);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveMovie_ValidMovie_RemovesMovieAndReturnsTrue()
        {
            // Arrange
            var movie = new Movie { Id = 4, Name = "Movie to Remove" };
            var mockDbSet = new Mock<DbSet<Movie>>();
            _mockContext.Setup(c => c.Movies).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync()).ReturnsAsync(1);
            _mockContext.Setup(c => c.Movies.FindAsync(movie.Id)).ReturnsAsync(movie);

            // Act
            var result = await _movieService.RemoveMovie(movie);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetMovieById_ValidId_ReturnsMovie()
        {
            // Arrange
            var movie = new Movie { Id = 5, Name = "Existing Movie" };
            var mockDbSet = new Mock<DbSet<Movie>>();
            _mockContext.Setup(c => c.Movies).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.Movies.Include(It.IsAny<string>())).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.Movies.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Movie, bool>>>())).ReturnsAsync(movie);

            // Act
            var result = await _movieService.GetMovieById(5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movie.Id, result.Id);
        }

        [Fact]
        public async Task GetGenresByIds_ValidIds_ReturnsGenres()
        {
            // Arrange
            var genreIds = new List<int> { 1, 2 };
            var genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" }
            };

            _mockGenreRepository.Setup(r => r.GetGenres()).ReturnsAsync(genres);

            // Act
            var result = await _movieService.GetGenresByIds(genreIds);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Id == 1);
            Assert.Contains(result, g => g.Id == 2);
        }

        [Fact]
        public async Task GetMovies_FiltersByGenresAndRatings_ReturnsFilteredMovies()
        {
            // Arrange
            var genreIds = new List<int> { 1 };
            var ratingIds = new List<int> { 2 };
            var movie = new Movie { Id = 6, Name = "Filtered Movie", RatingId = 2, Genres = new List<Genre> { new Genre { Id = 1 } } };
            var movieList = new List<Movie> { movie }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Movie>>();
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(movieList.Provider);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(movieList.Expression);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(movieList.ElementType);
            mockDbSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(movieList.GetEnumerator());

            _mockContext.Setup(c => c.Movies).Returns(mockDbSet.Object);

            // Act
            var result = await _movieService.GetMovies(genreIds, ratingIds);

            // Assert
            Assert.Single(result);
            Assert.Equal(movie.Id, result.First().Id);
        }
    }
}
