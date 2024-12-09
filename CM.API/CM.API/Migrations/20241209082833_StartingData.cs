using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class StartingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" },
                    { 4, "Horror" },
                    { 5, "Sci-Fi" },
                    { 6, "Romance" },
                    { 7, "Thriller" },
                    { 8, "Fantasy" },
                    { 9, "Documentary" },
                    { 10, "Animation" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DateReleased", "Description", "ImageUrl", "RatingId", "Title" },
                values: new object[,]
                {
                    { 11, new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "A thief who steals corporate secrets through the use of dream-sharing technology.", "https://static1.srcdn.com/wordpress/wp-content/uploads/2019/12/Inception-movie-poster-1.jpg", 3, "Inception" },
                    { 12, new DateTime(1999, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.", "https://www.syfy.com/sites/syfy/files/2021/03/the-matrix.jpeg", 4, "The Matrix" },
                    { 13, new DateTime(1972, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "https://ntvb.tmsimg.com/assets/p6326_v_h8_be.jpg?w=1280&h=720", 4, "The Godfather" },
                    { 14, new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.", "https://theconsultingdetectivesblog.com/wp-content/uploads/2014/06/the-dark-knight-original.jpg", 4, "The Dark Knight" },
                    { 15, new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", "https://waterfire.org/wp-content/uploads/2020/12/maxresdefault-5-1024x576.jpg", 4, "Pulp Fiction" },
                    { 16, new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold from the perspective of an Alabama man with an IQ of 75.", "https://you.stonybrook.edu/mysocproject/files/2020/03/Forrest-Gump.jpg", 2, "Forrest Gump" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$IIcV4QBCu80Xzt/bmnu/1OKEMKOHZKuOeWLmBxaAP2xFn7fIL3pH.");

            migrationBuilder.InsertData(
                table: "Showtime",
                columns: new[] { "Id", "Capacity", "MovieId", "StartTime", "TicketsAvailable" },
                values: new object[,]
                {
                    { 1, 25, 11, new DateTime(2024, 12, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 2, 25, 11, new DateTime(2024, 12, 14, 11, 30, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 3, 25, 12, new DateTime(2024, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 4, 25, 12, new DateTime(2024, 12, 15, 14, 15, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 5, 25, 13, new DateTime(2024, 12, 16, 16, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 6, 25, 13, new DateTime(2024, 12, 16, 16, 30, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 7, 25, 14, new DateTime(2024, 12, 18, 10, 15, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 8, 25, 14, new DateTime(2023, 12, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 9, 25, 15, new DateTime(2023, 12, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 10, 25, 15, new DateTime(2023, 12, 16, 17, 30, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 11, 25, 16, new DateTime(2023, 12, 26, 14, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 12, 25, 16, new DateTime(2023, 12, 26, 17, 0, 0, 0, DateTimeKind.Unspecified), 25 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Showtime",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$vbXuult0IdDnMxg9PQpVW.a8Ljj1qsQC9c4e7VameIM3jq/DibeDm");
        }
    }
}
