using Microsoft.EntityFrameworkCore;
using CM.API.Data;
using CM.API.Models;
using System;

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
            context.Users.Add(new User
            {
                Id = 1,
                Email = "test@example.com",
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"), // Hashed password
                DateOfBirth = DateTime.Now.AddYears(-25)
            });

            context.SaveChanges();
        }
    }
}
