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
    public DbSet<Showtime> Showtime { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<OrderResult> OrderResult { get; set; }
    public DbSet<OrderTicket> OrderTickets { get; set; }
    public DbSet<PaymentDetails> PaymentDetails { get; set; }
    public DbSet<CheckoutRequest> CheckoutRequest { get; set; }

    // this is used to further configure the model
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // This makes the title required
        modelBuilder.Entity<Movie>()
            .Property(m => m.Title)
            .IsRequired();
        modelBuilder.Entity<PaymentDetails>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

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

        modelBuilder.Entity<Showtime>()
           .HasMany(s => s.Tickets)
           .WithOne(t => t.Showtime)
           .HasForeignKey(t => t.ShowtimeId);

        modelBuilder.Entity<Showtime>()
             .HasOne(s => s.Movie)
             .WithMany(m => m.Showtimes)
             .HasForeignKey(s => s.MovieId);

        modelBuilder.Entity<User>()
             .HasOne(u => u.Cart)
             .WithOne(c => c.User)
             .HasForeignKey<Cart>(c => c.UserId);

        modelBuilder.Entity<OrderResult>()
            .HasMany(o => o.Tickets)
            .WithOne()
            .HasForeignKey(ot => ot.OrderTicketId);

        modelBuilder.Entity<OrderResult>()
                .HasOne(o => o.User)
                .WithMany(u => u.OrderResults)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderTicket>()
                .HasOne(ot => ot.Showtime)
                .WithMany()
                .HasForeignKey(ot => ot.ShowtimeId);

        modelBuilder.Entity<OrderTicket>()
                .HasOne(ot => ot.Movie)
                .WithMany()
                .HasForeignKey(ot => ot.MovieId);
    }
}