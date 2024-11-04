using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieIdToOrderTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "OrderTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrderResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderTickets_MovieId",
                table: "OrderTickets",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTickets_ShowtimeId",
                table: "OrderTickets",
                column: "ShowtimeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderResult_UserId",
                table: "OrderResult",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderResult_Users_UserId",
                table: "OrderResult",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTickets_Movies_MovieId",
                table: "OrderTickets",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTickets_Showtime_ShowtimeId",
                table: "OrderTickets",
                column: "ShowtimeId",
                principalTable: "Showtime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderResult_Users_UserId",
                table: "OrderResult");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTickets_Movies_MovieId",
                table: "OrderTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTickets_Showtime_ShowtimeId",
                table: "OrderTickets");

            migrationBuilder.DropIndex(
                name: "IX_OrderTickets_MovieId",
                table: "OrderTickets");

            migrationBuilder.DropIndex(
                name: "IX_OrderTickets_ShowtimeId",
                table: "OrderTickets");

            migrationBuilder.DropIndex(
                name: "IX_OrderResult_UserId",
                table: "OrderResult");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "OrderTickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderResult");
        }
    }
}
