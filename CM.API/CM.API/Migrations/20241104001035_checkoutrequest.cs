using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class checkoutrequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckoutRequestsId",
                table: "CheckoutRequest",
                newName: "PaymentDetailsId");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "CheckoutRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutRequest_PaymentDetailsId",
                table: "CheckoutRequest",
                column: "PaymentDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutRequest_PaymentDetails_PaymentDetailsId",
                table: "CheckoutRequest",
                column: "PaymentDetailsId",
                principalTable: "PaymentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutRequest_PaymentDetails_PaymentDetailsId",
                table: "CheckoutRequest");

            migrationBuilder.DropIndex(
                name: "IX_CheckoutRequest_PaymentDetailsId",
                table: "CheckoutRequest");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CheckoutRequest");

            migrationBuilder.RenameColumn(
                name: "PaymentDetailsId",
                table: "CheckoutRequest",
                newName: "CheckoutRequestsId");
        }
    }
}
