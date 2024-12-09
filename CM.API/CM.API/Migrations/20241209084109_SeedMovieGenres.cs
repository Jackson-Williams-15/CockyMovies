using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedMovieGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "GenresId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 14 },
                    { 3, 13 },
                    { 3, 15 },
                    { 3, 16 },
                    { 5, 11 },
                    { 5, 12 },
                    { 6, 16 },
                    { 7, 14 },
                    { 7, 15 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$2OKEFt137lXJaXBgzfVaQer8HLsdcUPGtrfyjN6eDR2MVqySB2J22");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 3, 15 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 3, 16 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 5, 11 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 5, 12 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 6, 16 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 7, 14 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { 7, 15 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$IIcV4QBCu80Xzt/bmnu/1OKEMKOHZKuOeWLmBxaAP2xFn7fIL3pH.");
        }
    }
}
