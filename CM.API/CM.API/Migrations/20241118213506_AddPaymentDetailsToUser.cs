using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentDetailsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PaymentDetailsId",
                table: "Users",
                column: "PaymentDetailsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PaymentDetails_PaymentDetailsId",
                table: "Users",
                column: "PaymentDetailsId",
                principalTable: "PaymentDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PaymentDetails_PaymentDetailsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PaymentDetailsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PaymentDetailsId",
                table: "Users");
        }
    }
}
