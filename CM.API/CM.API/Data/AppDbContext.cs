using Microsoft.EntityFrameworkCore;
using CM.API.Models;

namespace CM.API.Data;
public class AppDbContext : DbContext
{
    // constructor that passes options to the base DbContext class
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet for entities
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rating> Ratings { get; set; }


    // this is used to further configure the model
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // This makes the title required
        modelBuilder.Entity<Movie>()
            .Property(m => m.Title)
            .IsRequired();

        modelBuilder.Entity<Movie>()
       .HasMany(m => m.Genres)
       .WithMany(g => g.Movies)
       .UsingEntity(j => j.ToTable("MovieGenres"));

       modelBuilder.Entity<Movie>()
                .HasOne(m => m.Rating)
                .WithMany()
                .HasForeignKey(m => m.RatingId);

        // User entity
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.DateOfBirth)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired();

    // Data seed ratings
    modelBuilder.Entity<Rating>().HasData(
        new Rating { Id = 1, Name = "G" },
        new Rating { Id = 2, Name = "PG" },
        new Rating { Id = 3, Name = "PG-13" },
        new Rating { Id = 4, Name = "R" },
        new Rating { Id = 5, Name = "NC-17" }
    );
    }
}