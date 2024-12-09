using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ticket",
                columns: new[] { "Id", "CartId", "IsSold", "Price", "Quantity", "ShowtimeId" },
                values: new object[,]
                {
                    { 1, null, false, 10.00m, 0, 1 },
                    { 2, null, false, 10.00m, 0, 1 },
                    { 3, null, false, 10.00m, 0, 1 },
                    { 4, null, false, 10.00m, 0, 1 },
                    { 5, null, false, 10.00m, 0, 1 },
                    { 6, null, false, 10.00m, 0, 1 },
                    { 7, null, false, 10.00m, 0, 1 },
                    { 8, null, false, 10.00m, 0, 1 },
                    { 9, null, false, 10.00m, 0, 1 },
                    { 10, null, false, 10.00m, 0, 1 },
                    { 11, null, false, 10.00m, 0, 1 },
                    { 12, null, false, 10.00m, 0, 1 },
                    { 13, null, false, 10.00m, 0, 1 },
                    { 14, null, false, 10.00m, 0, 1 },
                    { 15, null, false, 10.00m, 0, 1 },
                    { 16, null, false, 10.00m, 0, 1 },
                    { 17, null, false, 10.00m, 0, 1 },
                    { 18, null, false, 10.00m, 0, 1 },
                    { 19, null, false, 10.00m, 0, 1 },
                    { 20, null, false, 10.00m, 0, 1 },
                    { 21, null, false, 10.00m, 0, 1 },
                    { 22, null, false, 10.00m, 0, 1 },
                    { 23, null, false, 10.00m, 0, 1 },
                    { 24, null, false, 10.00m, 0, 1 },
                    { 25, null, false, 10.00m, 0, 1 },
                    { 26, null, false, 10.00m, 0, 2 },
                    { 27, null, false, 10.00m, 0, 2 },
                    { 28, null, false, 10.00m, 0, 2 },
                    { 29, null, false, 10.00m, 0, 2 },
                    { 30, null, false, 10.00m, 0, 2 },
                    { 31, null, false, 10.00m, 0, 2 },
                    { 32, null, false, 10.00m, 0, 2 },
                    { 33, null, false, 10.00m, 0, 2 },
                    { 34, null, false, 10.00m, 0, 2 },
                    { 35, null, false, 10.00m, 0, 2 },
                    { 36, null, false, 10.00m, 0, 2 },
                    { 37, null, false, 10.00m, 0, 2 },
                    { 38, null, false, 10.00m, 0, 2 },
                    { 39, null, false, 10.00m, 0, 2 },
                    { 40, null, false, 10.00m, 0, 2 },
                    { 41, null, false, 10.00m, 0, 2 },
                    { 42, null, false, 10.00m, 0, 2 },
                    { 43, null, false, 10.00m, 0, 2 },
                    { 44, null, false, 10.00m, 0, 2 },
                    { 45, null, false, 10.00m, 0, 2 },
                    { 46, null, false, 10.00m, 0, 2 },
                    { 47, null, false, 10.00m, 0, 2 },
                    { 48, null, false, 10.00m, 0, 2 },
                    { 49, null, false, 10.00m, 0, 2 },
                    { 50, null, false, 10.00m, 0, 2 },
                    { 51, null, false, 10.00m, 0, 3 },
                    { 52, null, false, 10.00m, 0, 3 },
                    { 53, null, false, 10.00m, 0, 3 },
                    { 54, null, false, 10.00m, 0, 3 },
                    { 55, null, false, 10.00m, 0, 3 },
                    { 56, null, false, 10.00m, 0, 3 },
                    { 57, null, false, 10.00m, 0, 3 },
                    { 58, null, false, 10.00m, 0, 3 },
                    { 59, null, false, 10.00m, 0, 3 },
                    { 60, null, false, 10.00m, 0, 3 },
                    { 61, null, false, 10.00m, 0, 3 },
                    { 62, null, false, 10.00m, 0, 3 },
                    { 63, null, false, 10.00m, 0, 3 },
                    { 64, null, false, 10.00m, 0, 3 },
                    { 65, null, false, 10.00m, 0, 3 },
                    { 66, null, false, 10.00m, 0, 3 },
                    { 67, null, false, 10.00m, 0, 3 },
                    { 68, null, false, 10.00m, 0, 3 },
                    { 69, null, false, 10.00m, 0, 3 },
                    { 70, null, false, 10.00m, 0, 3 },
                    { 71, null, false, 10.00m, 0, 3 },
                    { 72, null, false, 10.00m, 0, 3 },
                    { 73, null, false, 10.00m, 0, 3 },
                    { 74, null, false, 10.00m, 0, 3 },
                    { 75, null, false, 10.00m, 0, 3 },
                    { 76, null, false, 10.00m, 0, 4 },
                    { 77, null, false, 10.00m, 0, 4 },
                    { 78, null, false, 10.00m, 0, 4 },
                    { 79, null, false, 10.00m, 0, 4 },
                    { 80, null, false, 10.00m, 0, 4 },
                    { 81, null, false, 10.00m, 0, 4 },
                    { 82, null, false, 10.00m, 0, 4 },
                    { 83, null, false, 10.00m, 0, 4 },
                    { 84, null, false, 10.00m, 0, 4 },
                    { 85, null, false, 10.00m, 0, 4 },
                    { 86, null, false, 10.00m, 0, 4 },
                    { 87, null, false, 10.00m, 0, 4 },
                    { 88, null, false, 10.00m, 0, 4 },
                    { 89, null, false, 10.00m, 0, 4 },
                    { 90, null, false, 10.00m, 0, 4 },
                    { 91, null, false, 10.00m, 0, 4 },
                    { 92, null, false, 10.00m, 0, 4 },
                    { 93, null, false, 10.00m, 0, 4 },
                    { 94, null, false, 10.00m, 0, 4 },
                    { 95, null, false, 10.00m, 0, 4 },
                    { 96, null, false, 10.00m, 0, 4 },
                    { 97, null, false, 10.00m, 0, 4 },
                    { 98, null, false, 10.00m, 0, 4 },
                    { 99, null, false, 10.00m, 0, 4 },
                    { 100, null, false, 10.00m, 0, 4 },
                    { 101, null, false, 10.00m, 0, 5 },
                    { 102, null, false, 10.00m, 0, 5 },
                    { 103, null, false, 10.00m, 0, 5 },
                    { 104, null, false, 10.00m, 0, 5 },
                    { 105, null, false, 10.00m, 0, 5 },
                    { 106, null, false, 10.00m, 0, 5 },
                    { 107, null, false, 10.00m, 0, 5 },
                    { 108, null, false, 10.00m, 0, 5 },
                    { 109, null, false, 10.00m, 0, 5 },
                    { 110, null, false, 10.00m, 0, 5 },
                    { 111, null, false, 10.00m, 0, 5 },
                    { 112, null, false, 10.00m, 0, 5 },
                    { 113, null, false, 10.00m, 0, 5 },
                    { 114, null, false, 10.00m, 0, 5 },
                    { 115, null, false, 10.00m, 0, 5 },
                    { 116, null, false, 10.00m, 0, 5 },
                    { 117, null, false, 10.00m, 0, 5 },
                    { 118, null, false, 10.00m, 0, 5 },
                    { 119, null, false, 10.00m, 0, 5 },
                    { 120, null, false, 10.00m, 0, 5 },
                    { 121, null, false, 10.00m, 0, 5 },
                    { 122, null, false, 10.00m, 0, 5 },
                    { 123, null, false, 10.00m, 0, 5 },
                    { 124, null, false, 10.00m, 0, 5 },
                    { 125, null, false, 10.00m, 0, 5 },
                    { 126, null, false, 10.00m, 0, 6 },
                    { 127, null, false, 10.00m, 0, 6 },
                    { 128, null, false, 10.00m, 0, 6 },
                    { 129, null, false, 10.00m, 0, 6 },
                    { 130, null, false, 10.00m, 0, 6 },
                    { 131, null, false, 10.00m, 0, 6 },
                    { 132, null, false, 10.00m, 0, 6 },
                    { 133, null, false, 10.00m, 0, 6 },
                    { 134, null, false, 10.00m, 0, 6 },
                    { 135, null, false, 10.00m, 0, 6 },
                    { 136, null, false, 10.00m, 0, 6 },
                    { 137, null, false, 10.00m, 0, 6 },
                    { 138, null, false, 10.00m, 0, 6 },
                    { 139, null, false, 10.00m, 0, 6 },
                    { 140, null, false, 10.00m, 0, 6 },
                    { 141, null, false, 10.00m, 0, 6 },
                    { 142, null, false, 10.00m, 0, 6 },
                    { 143, null, false, 10.00m, 0, 6 },
                    { 144, null, false, 10.00m, 0, 6 },
                    { 145, null, false, 10.00m, 0, 6 },
                    { 146, null, false, 10.00m, 0, 6 },
                    { 147, null, false, 10.00m, 0, 6 },
                    { 148, null, false, 10.00m, 0, 6 },
                    { 149, null, false, 10.00m, 0, 6 },
                    { 150, null, false, 10.00m, 0, 6 },
                    { 151, null, false, 10.00m, 0, 7 },
                    { 152, null, false, 10.00m, 0, 7 },
                    { 153, null, false, 10.00m, 0, 7 },
                    { 154, null, false, 10.00m, 0, 7 },
                    { 155, null, false, 10.00m, 0, 7 },
                    { 156, null, false, 10.00m, 0, 7 },
                    { 157, null, false, 10.00m, 0, 7 },
                    { 158, null, false, 10.00m, 0, 7 },
                    { 159, null, false, 10.00m, 0, 7 },
                    { 160, null, false, 10.00m, 0, 7 },
                    { 161, null, false, 10.00m, 0, 7 },
                    { 162, null, false, 10.00m, 0, 7 },
                    { 163, null, false, 10.00m, 0, 7 },
                    { 164, null, false, 10.00m, 0, 7 },
                    { 165, null, false, 10.00m, 0, 7 },
                    { 166, null, false, 10.00m, 0, 7 },
                    { 167, null, false, 10.00m, 0, 7 },
                    { 168, null, false, 10.00m, 0, 7 },
                    { 169, null, false, 10.00m, 0, 7 },
                    { 170, null, false, 10.00m, 0, 7 },
                    { 171, null, false, 10.00m, 0, 7 },
                    { 172, null, false, 10.00m, 0, 7 },
                    { 173, null, false, 10.00m, 0, 7 },
                    { 174, null, false, 10.00m, 0, 7 },
                    { 175, null, false, 10.00m, 0, 7 },
                    { 176, null, false, 10.00m, 0, 8 },
                    { 177, null, false, 10.00m, 0, 8 },
                    { 178, null, false, 10.00m, 0, 8 },
                    { 179, null, false, 10.00m, 0, 8 },
                    { 180, null, false, 10.00m, 0, 8 },
                    { 181, null, false, 10.00m, 0, 8 },
                    { 182, null, false, 10.00m, 0, 8 },
                    { 183, null, false, 10.00m, 0, 8 },
                    { 184, null, false, 10.00m, 0, 8 },
                    { 185, null, false, 10.00m, 0, 8 },
                    { 186, null, false, 10.00m, 0, 8 },
                    { 187, null, false, 10.00m, 0, 8 },
                    { 188, null, false, 10.00m, 0, 8 },
                    { 189, null, false, 10.00m, 0, 8 },
                    { 190, null, false, 10.00m, 0, 8 },
                    { 191, null, false, 10.00m, 0, 8 },
                    { 192, null, false, 10.00m, 0, 8 },
                    { 193, null, false, 10.00m, 0, 8 },
                    { 194, null, false, 10.00m, 0, 8 },
                    { 195, null, false, 10.00m, 0, 8 },
                    { 196, null, false, 10.00m, 0, 8 },
                    { 197, null, false, 10.00m, 0, 8 },
                    { 198, null, false, 10.00m, 0, 8 },
                    { 199, null, false, 10.00m, 0, 8 },
                    { 200, null, false, 10.00m, 0, 8 },
                    { 201, null, false, 10.00m, 0, 9 },
                    { 202, null, false, 10.00m, 0, 9 },
                    { 203, null, false, 10.00m, 0, 9 },
                    { 204, null, false, 10.00m, 0, 9 },
                    { 205, null, false, 10.00m, 0, 9 },
                    { 206, null, false, 10.00m, 0, 9 },
                    { 207, null, false, 10.00m, 0, 9 },
                    { 208, null, false, 10.00m, 0, 9 },
                    { 209, null, false, 10.00m, 0, 9 },
                    { 210, null, false, 10.00m, 0, 9 },
                    { 211, null, false, 10.00m, 0, 9 },
                    { 212, null, false, 10.00m, 0, 9 },
                    { 213, null, false, 10.00m, 0, 9 },
                    { 214, null, false, 10.00m, 0, 9 },
                    { 215, null, false, 10.00m, 0, 9 },
                    { 216, null, false, 10.00m, 0, 9 },
                    { 217, null, false, 10.00m, 0, 9 },
                    { 218, null, false, 10.00m, 0, 9 },
                    { 219, null, false, 10.00m, 0, 9 },
                    { 220, null, false, 10.00m, 0, 9 },
                    { 221, null, false, 10.00m, 0, 9 },
                    { 222, null, false, 10.00m, 0, 9 },
                    { 223, null, false, 10.00m, 0, 9 },
                    { 224, null, false, 10.00m, 0, 9 },
                    { 225, null, false, 10.00m, 0, 9 },
                    { 226, null, false, 10.00m, 0, 10 },
                    { 227, null, false, 10.00m, 0, 10 },
                    { 228, null, false, 10.00m, 0, 10 },
                    { 229, null, false, 10.00m, 0, 10 },
                    { 230, null, false, 10.00m, 0, 10 },
                    { 231, null, false, 10.00m, 0, 10 },
                    { 232, null, false, 10.00m, 0, 10 },
                    { 233, null, false, 10.00m, 0, 10 },
                    { 234, null, false, 10.00m, 0, 10 },
                    { 235, null, false, 10.00m, 0, 10 },
                    { 236, null, false, 10.00m, 0, 10 },
                    { 237, null, false, 10.00m, 0, 10 },
                    { 238, null, false, 10.00m, 0, 10 },
                    { 239, null, false, 10.00m, 0, 10 },
                    { 240, null, false, 10.00m, 0, 10 },
                    { 241, null, false, 10.00m, 0, 10 },
                    { 242, null, false, 10.00m, 0, 10 },
                    { 243, null, false, 10.00m, 0, 10 },
                    { 244, null, false, 10.00m, 0, 10 },
                    { 245, null, false, 10.00m, 0, 10 },
                    { 246, null, false, 10.00m, 0, 10 },
                    { 247, null, false, 10.00m, 0, 10 },
                    { 248, null, false, 10.00m, 0, 10 },
                    { 249, null, false, 10.00m, 0, 10 },
                    { 250, null, false, 10.00m, 0, 10 },
                    { 251, null, false, 10.00m, 0, 11 },
                    { 252, null, false, 10.00m, 0, 11 },
                    { 253, null, false, 10.00m, 0, 11 },
                    { 254, null, false, 10.00m, 0, 11 },
                    { 255, null, false, 10.00m, 0, 11 },
                    { 256, null, false, 10.00m, 0, 11 },
                    { 257, null, false, 10.00m, 0, 11 },
                    { 258, null, false, 10.00m, 0, 11 },
                    { 259, null, false, 10.00m, 0, 11 },
                    { 260, null, false, 10.00m, 0, 11 },
                    { 261, null, false, 10.00m, 0, 11 },
                    { 262, null, false, 10.00m, 0, 11 },
                    { 263, null, false, 10.00m, 0, 11 },
                    { 264, null, false, 10.00m, 0, 11 },
                    { 265, null, false, 10.00m, 0, 11 },
                    { 266, null, false, 10.00m, 0, 11 },
                    { 267, null, false, 10.00m, 0, 11 },
                    { 268, null, false, 10.00m, 0, 11 },
                    { 269, null, false, 10.00m, 0, 11 },
                    { 270, null, false, 10.00m, 0, 11 },
                    { 271, null, false, 10.00m, 0, 11 },
                    { 272, null, false, 10.00m, 0, 11 },
                    { 273, null, false, 10.00m, 0, 11 },
                    { 274, null, false, 10.00m, 0, 11 },
                    { 275, null, false, 10.00m, 0, 11 },
                    { 276, null, false, 10.00m, 0, 12 },
                    { 277, null, false, 10.00m, 0, 12 },
                    { 278, null, false, 10.00m, 0, 12 },
                    { 279, null, false, 10.00m, 0, 12 },
                    { 280, null, false, 10.00m, 0, 12 },
                    { 281, null, false, 10.00m, 0, 12 },
                    { 282, null, false, 10.00m, 0, 12 },
                    { 283, null, false, 10.00m, 0, 12 },
                    { 284, null, false, 10.00m, 0, 12 },
                    { 285, null, false, 10.00m, 0, 12 },
                    { 286, null, false, 10.00m, 0, 12 },
                    { 287, null, false, 10.00m, 0, 12 },
                    { 288, null, false, 10.00m, 0, 12 },
                    { 289, null, false, 10.00m, 0, 12 },
                    { 290, null, false, 10.00m, 0, 12 },
                    { 291, null, false, 10.00m, 0, 12 },
                    { 292, null, false, 10.00m, 0, 12 },
                    { 293, null, false, 10.00m, 0, 12 },
                    { 294, null, false, 10.00m, 0, 12 },
                    { 295, null, false, 10.00m, 0, 12 },
                    { 296, null, false, 10.00m, 0, 12 },
                    { 297, null, false, 10.00m, 0, 12 },
                    { 298, null, false, 10.00m, 0, 12 },
                    { 299, null, false, 10.00m, 0, 12 },
                    { 300, null, false, 10.00m, 0, 12 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$Mgf2XgOYnWhRLxtRpm5lTO88phZtdd1g5B6cpLNjk2VUAfbgLgppu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$2OKEFt137lXJaXBgzfVaQer8HLsdcUPGtrfyjN6eDR2MVqySB2J22");
        }
    }
}
