using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderResultIdToOrderTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderResultId",
                table: "OrderTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderTickets_OrderResultId",
                table: "OrderTickets",
                column: "OrderResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTickets_OrderResult_OrderResultId",
                table: "OrderTickets",
                column: "OrderResultId",
                principalTable: "OrderResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTickets_OrderResult_OrderResultId",
                table: "OrderTickets");

            migrationBuilder.DropIndex(
                name: "IX_OrderTickets_OrderResultId",
                table: "OrderTickets");

            migrationBuilder.DropColumn(
                name: "OrderResultId",
                table: "OrderTickets");
        }
    }
}
