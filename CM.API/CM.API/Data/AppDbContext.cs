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
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reply> Reply { get; set; }

    // this is used to further configure the model
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply data seeding
            modelBuilder.Seed();

        // This makes the title required
        modelBuilder.Entity<Movie>()
            .Property(m => m.Title)
            .IsRequired();
        modelBuilder.Entity<PaymentDetails>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

        // Configure many-to-many relationship between Movie and Genre
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity<Dictionary<string, object>>(
                "MovieGenres",
                j => j.HasOne<Genre>().WithMany().HasForeignKey("GenresId"),
                j => j.HasOne<Movie>().WithMany().HasForeignKey("MoviesId")
            );

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

        modelBuilder.Entity<User>()
            .HasOne(u => u.PaymentDetails)
            .WithOne()
            .HasForeignKey<User>(u => u.PaymentDetailsId);

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

        modelBuilder.Entity<Review>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId);

        modelBuilder.Entity<Reply>()
            .HasOne(r => r.Review)
            .WithMany(rev => rev.Reply)
            .HasForeignKey(r => r.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reply>()
            .Property(r => r.Author)
            .IsRequired();

        modelBuilder.Entity<Reply>()
            .Property(r => r.Body)
            .IsRequired();
    }
}