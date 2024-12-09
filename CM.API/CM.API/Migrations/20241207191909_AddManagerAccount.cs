using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "Password", "PaymentDetailsId", "Role", "Username" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "managerEmail@example.com", "$2a$11$vbXuult0IdDnMxg9PQpVW.a8Ljj1qsQC9c4e7VameIM3jq/DibeDm", null, "Manager", "Manager" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "Password", "PaymentDetailsId", "Role", "Username" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "manager@example.com", "$2a$11$EUtMRnl5ohn4vQ/FxUbr7uw0Ni/J/VYfpRopgVxSIQaEVd4.wzTGm", null, "Manager", "manager" });
        }
    }
}
