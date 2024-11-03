using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketsAvailableToShowtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketsAvailable",
                table: "Showtime",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketsAvailable",
                table: "Showtime");
        }
    }
}
