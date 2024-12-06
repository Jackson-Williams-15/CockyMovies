using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Repositories;
using CM.API.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CM.API.Tests
{
    public class GenreServiceTests
    {
        private readonly Mock<GenreRepository> _mockGenreRepository;
        private readonly GenreService _genreService;

        public GenreServiceTests()
        {
            _mockGenreRepository = new Mock<GenreRepository>();
            _genreService = new GenreService(_mockGenreRepository.Object);
        }

        [Fact]
        public async Task GetGenres_ReturnsListOfMovieGenres()
        {
            // Arrange
            var expectedGenres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" }
            };

            _mockGenreRepository.Setup(repo => repo.GetGenres()).ReturnsAsync(expectedGenres);

            // Act
            var result = await _genreService.GetGenres();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Action", result[0].Name);
            Assert.Equal("Comedy", result[1].Name);
            Assert.Equal("Drama", result[2].Name);
        }

        [Fact]
        public async Task GetGenreById_ReturnsMovieGenre_WhenFound()
        {
            // Arrange
            var genreId = 2;
            var expectedGenre = new Genre { Id = genreId, Name = "Comedy" };

            _mockGenreRepository.Setup(repo => repo.GetGenreById(genreId)).ReturnsAsync(expectedGenre);

            // Act
            var result = await _genreService.GetGenreById(genreId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(genreId, result.Id);
            Assert.Equal("Comedy", result.Name);
        }

        [Fact]
        public async Task GetGenreById_ReturnsNull_WhenGenreNotFound()
        {
            // Arrange
            var genreId = 999; // ID not in the mock data
            _mockGenreRepository.Setup(repo => repo.GetGenreById(genreId)).ReturnsAsync((Genre)null);

            // Act
            var result = await _genreService.GetGenreById(genreId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddGenre_ReturnsTrue_WhenGenreIsAddedSuccessfully()
        {
            // Arrange
            var newGenre = new Genre { Name = "Sci-Fi" };

            _mockGenreRepository.Setup(repo => repo.AddGenre(newGenre)).ReturnsAsync(true);

            // Act
            var result = await _genreService.AddGenre(newGenre);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddGenre_ReturnsFalse_WhenGenreAdditionFails()
        {
            // Arrange
            var newGenre = new Genre { Name = "Sci-Fi" };

            _mockGenreRepository.Setup(repo => repo.AddGenre(newGenre)).ReturnsAsync(false);

            // Act
            var result = await _genreService.AddGenre(newGenre);

            // Assert
            Assert.False(result);
        }
    }
}
