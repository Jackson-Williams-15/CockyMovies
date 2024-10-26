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
    }
}