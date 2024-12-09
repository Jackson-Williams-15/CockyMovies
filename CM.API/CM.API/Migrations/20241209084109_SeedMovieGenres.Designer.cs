﻿// <auto-generated />
using System;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CM.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241209084109_SeedMovieGenres")]
    partial class SeedMovieGenres
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CM.API.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("CM.API.Models.CheckoutRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentDetailsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentDetailsId");

                    b.ToTable("CheckoutRequest");
                });

            modelBuilder.Entity("CM.API.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Drama"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Sci-Fi"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Thriller"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Documentary"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Animation"
                        });
                });

            modelBuilder.Entity("CM.API.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateReleased")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RatingId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RatingId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 11,
                            DateReleased = new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A thief who steals corporate secrets through the use of dream-sharing technology.",
                            ImageUrl = "https://static1.srcdn.com/wordpress/wp-content/uploads/2019/12/Inception-movie-poster-1.jpg",
                            RatingId = 3,
                            Title = "Inception"
                        },
                        new
                        {
                            Id = 12,
                            DateReleased = new DateTime(1999, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.",
                            ImageUrl = "https://www.syfy.com/sites/syfy/files/2021/03/the-matrix.jpeg",
                            RatingId = 4,
                            Title = "The Matrix"
                        },
                        new
                        {
                            Id = 13,
                            DateReleased = new DateTime(1972, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                            ImageUrl = "https://ntvb.tmsimg.com/assets/p6326_v_h8_be.jpg?w=1280&h=720",
                            RatingId = 4,
                            Title = "The Godfather"
                        },
                        new
                        {
                            Id = 14,
                            DateReleased = new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                            ImageUrl = "https://theconsultingdetectivesblog.com/wp-content/uploads/2014/06/the-dark-knight-original.jpg",
                            RatingId = 4,
                            Title = "The Dark Knight"
                        },
                        new
                        {
                            Id = 15,
                            DateReleased = new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                            ImageUrl = "https://waterfire.org/wp-content/uploads/2020/12/maxresdefault-5-1024x576.jpg",
                            RatingId = 4,
                            Title = "Pulp Fiction"
                        },
                        new
                        {
                            Id = 16,
                            DateReleased = new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold from the perspective of an Alabama man with an IQ of 75.",
                            ImageUrl = "https://you.stonybrook.edu/mysocproject/files/2020/03/Forrest-Gump.jpg",
                            RatingId = 2,
                            Title = "Forrest Gump"
                        });
                });

            modelBuilder.Entity("CM.API.Models.OrderResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ProcessedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Success")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OrderResult");
                });

            modelBuilder.Entity("CM.API.Models.OrderTicket", b =>
                {
                    b.Property<int>("OrderTicketId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("OrderResultId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShowtimeId")
                        .HasColumnType("int");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("OrderTicketId");

                    b.HasIndex("MovieId");

                    b.HasIndex("OrderResultId");

                    b.HasIndex("ShowtimeId");

                    b.ToTable("OrderTickets");
                });

            modelBuilder.Entity("CM.API.Models.PaymentDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ExpiryDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("CM.API.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "G"
                        },
                        new
                        {
                            Id = 2,
                            Name = "PG"
                        },
                        new
                        {
                            Id = 3,
                            Name = "PG-13"
                        },
                        new
                        {
                            Id = 4,
                            Name = "R"
                        },
                        new
                        {
                            Id = 5,
                            Name = "NC-17"
                        });
                });

            modelBuilder.Entity("CM.API.Models.Reply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ReviewId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("CM.API.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CM.API.Models.Showtime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TicketsAvailable")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Showtime");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 25,
                            MovieId = 11,
                            StartTime = new DateTime(2024, 12, 14, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 25,
                            MovieId = 11,
                            StartTime = new DateTime(2024, 12, 14, 11, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 25,
                            MovieId = 12,
                            StartTime = new DateTime(2024, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 4,
                            Capacity = 25,
                            MovieId = 12,
                            StartTime = new DateTime(2024, 12, 15, 14, 15, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 5,
                            Capacity = 25,
                            MovieId = 13,
                            StartTime = new DateTime(2024, 12, 16, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 6,
                            Capacity = 25,
                            MovieId = 13,
                            StartTime = new DateTime(2024, 12, 16, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 7,
                            Capacity = 25,
                            MovieId = 14,
                            StartTime = new DateTime(2024, 12, 18, 10, 15, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 8,
                            Capacity = 25,
                            MovieId = 14,
                            StartTime = new DateTime(2023, 12, 18, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 9,
                            Capacity = 25,
                            MovieId = 15,
                            StartTime = new DateTime(2023, 12, 16, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 10,
                            Capacity = 25,
                            MovieId = 15,
                            StartTime = new DateTime(2023, 12, 16, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 11,
                            Capacity = 25,
                            MovieId = 16,
                            StartTime = new DateTime(2023, 12, 26, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        },
                        new
                        {
                            Id = 12,
                            Capacity = 25,
                            MovieId = 16,
                            StartTime = new DateTime(2023, 12, 26, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = 25
                        });
                });

            modelBuilder.Entity("CM.API.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSold")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShowtimeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ShowtimeId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("CM.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("PaymentDetailsId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentDetailsId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "managerEmail@example.com",
                            Password = "$2a$11$2OKEFt137lXJaXBgzfVaQer8HLsdcUPGtrfyjN6eDR2MVqySB2J22",
                            Role = "Manager",
                            Username = "Manager"
                        });
                });

            modelBuilder.Entity("MovieGenres", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("int");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.HasKey("GenresId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("MovieGenres");

                    b.HasData(
                        new
                        {
                            GenresId = 1,
                            MoviesId = 11
                        },
                        new
                        {
                            GenresId = 5,
                            MoviesId = 11
                        },
                        new
                        {
                            GenresId = 1,
                            MoviesId = 12
                        },
                        new
                        {
                            GenresId = 5,
                            MoviesId = 12
                        },
                        new
                        {
                            GenresId = 3,
                            MoviesId = 13
                        },
                        new
                        {
                            GenresId = 1,
                            MoviesId = 14
                        },
                        new
                        {
                            GenresId = 7,
                            MoviesId = 14
                        },
                        new
                        {
                            GenresId = 3,
                            MoviesId = 15
                        },
                        new
                        {
                            GenresId = 7,
                            MoviesId = 15
                        },
                        new
                        {
                            GenresId = 3,
                            MoviesId = 16
                        },
                        new
                        {
                            GenresId = 6,
                            MoviesId = 16
                        });
                });

            modelBuilder.Entity("CM.API.Models.Cart", b =>
                {
                    b.HasOne("CM.API.Models.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("CM.API.Models.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CM.API.Models.CheckoutRequest", b =>
                {
                    b.HasOne("CM.API.Models.PaymentDetails", "PaymentDetails")
                        .WithMany()
                        .HasForeignKey("PaymentDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("CM.API.Models.Movie", b =>
                {
                    b.HasOne("CM.API.Models.Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("CM.API.Models.OrderResult", b =>
                {
                    b.HasOne("CM.API.Models.User", "User")
                        .WithMany("OrderResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CM.API.Models.OrderTicket", b =>
                {
                    b.HasOne("CM.API.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CM.API.Models.OrderResult", "OrderResult")
                        .WithMany()
                        .HasForeignKey("OrderResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CM.API.Models.OrderResult", null)
                        .WithMany("Tickets")
                        .HasForeignKey("OrderTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CM.API.Models.Showtime", "Showtime")
                        .WithMany()
                        .HasForeignKey("ShowtimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("OrderResult");

                    b.Navigation("Showtime");
                });

            modelBuilder.Entity("CM.API.Models.Reply", b =>
                {
                    b.HasOne("CM.API.Models.Review", "Review")
                        .WithMany("Reply")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("CM.API.Models.Review", b =>
                {
                    b.HasOne("CM.API.Models.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CM.API.Models.Showtime", b =>
                {
                    b.HasOne("CM.API.Models.Movie", "Movie")
                        .WithMany("Showtimes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CM.API.Models.Ticket", b =>
                {
                    b.HasOne("CM.API.Models.Cart", null)
                        .WithMany("Tickets")
                        .HasForeignKey("CartId");

                    b.HasOne("CM.API.Models.Showtime", "Showtime")
                        .WithMany("Tickets")
                        .HasForeignKey("ShowtimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Showtime");
                });

            modelBuilder.Entity("CM.API.Models.User", b =>
                {
                    b.HasOne("CM.API.Models.PaymentDetails", "PaymentDetails")
                        .WithOne()
                        .HasForeignKey("CM.API.Models.User", "PaymentDetailsId");

                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("MovieGenres", b =>
                {
                    b.HasOne("CM.API.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CM.API.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CM.API.Models.Cart", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("CM.API.Models.Movie", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("CM.API.Models.OrderResult", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("CM.API.Models.Review", b =>
                {
                    b.Navigation("Reply");
                });

            modelBuilder.Entity("CM.API.Models.Showtime", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("CM.API.Models.User", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("OrderResults");
                });
#pragma warning restore 612, 618
        }
    }
}