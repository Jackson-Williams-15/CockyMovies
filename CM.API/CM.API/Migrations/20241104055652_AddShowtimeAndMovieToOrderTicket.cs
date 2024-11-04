using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddShowtimeAndMovieToOrderTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderTickets_ShowtimeId",
                table: "OrderTickets",
                column: "ShowtimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTickets_Movies_ShowtimeId",
                table: "OrderTickets",
                column: "ShowtimeId",
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
                name: "FK_OrderTickets_Movies_ShowtimeId",
                table: "OrderTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTickets_Showtime_ShowtimeId",
                table: "OrderTickets");

            migrationBuilder.DropIndex(
                name: "IX_OrderTickets_ShowtimeId",
                table: "OrderTickets");
        }
    }
}
