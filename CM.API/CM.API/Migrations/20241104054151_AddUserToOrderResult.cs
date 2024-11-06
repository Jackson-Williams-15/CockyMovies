using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToOrderResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrderResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderResult_Users_UserId",
                table: "OrderResult");

            migrationBuilder.DropIndex(
                name: "IX_OrderResult_UserId",
                table: "OrderResult");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderResult");
        }
    }
}
