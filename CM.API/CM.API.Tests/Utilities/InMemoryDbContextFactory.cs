using Microsoft.EntityFrameworkCore;
using CM.API.Data;
using CM.API.Models;
using System;
using System.Collections.Generic;

namespace CM.API.Tests.Utilities
{
    public class InMemoryDbContextFactory
    {
        public static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database per test
                .Options;

            var context = new AppDbContext(options);

            // Seed initial data if necessary
            SeedData(context);

            return context;
        }

        private static void SeedData(AppDbContext context)
        {
            // Seed user data
            context.Users.Add(new User
            {
                Id = 1,
                Email = "test@example.com",
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"), // Hashed password
                DateOfBirth = DateTime.Now.AddYears(-25)
            });

            // Seed movie data
            var movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                Description = "Test Description",
                DateReleased = DateTime.Now.AddMonths(-2),
                Rating = new Rating { Id = 1, Name = "PG-13" },
                Showtimes = new List<Showtime>()
            };
            context.Movies.Add(movie);

            // Seed showtime data
            var showtime = new Showtime
            {
                Id = 1,
                MovieId = movie.Id,
                StartTime = DateTime.Now,
                Movie = movie,
                Tickets = new List<Ticket>()
            };
            context.Showtime.Add(showtime);

            // Add showtime to movie
            movie.Showtimes.Add(showtime);

            // Seed cart data
            var cart = new Cart
            {
                CartId = 1,
                UserId = 1,
                Tickets = new List<Ticket>()
            };
            context.Carts.Add(cart);

            // Save changes to the in-memory database
            context.SaveChanges();
        }
    }
}
