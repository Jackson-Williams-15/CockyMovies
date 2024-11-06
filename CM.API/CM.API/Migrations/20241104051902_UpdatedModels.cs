using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderResultsId",
                table: "OrderResult");

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Ticket",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "OrderResult",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "OrderTickets",
                columns: table => new
                {
                    OrderTicketId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    ShowtimeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTickets", x => x.OrderTicketId);
                    table.ForeignKey(
                        name: "FK_OrderTickets_OrderResult_OrderTicketId",
                        column: x => x.OrderTicketId,
                        principalTable: "OrderResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTickets");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderResult");

            migrationBuilder.AddColumn<int>(
                name: "OrderResultsId",
                table: "OrderResult",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
