using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCartRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Showtime_Movies_MovieId",
                table: "Showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Showtime_ShowtimeId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Showtime",
                table: "Showtime");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "Showtime",
                newName: "Showtimes");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_ShowtimeId",
                table: "Tickets",
                newName: "IX_Tickets_ShowtimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtime_MovieId",
                table: "Showtimes",
                newName: "IX_Showtimes_MovieId");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showtimes",
                table: "Showtimes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CartId",
                table: "Tickets",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Movies_MovieId",
                table: "Showtimes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                table: "Tickets",
                column: "ShowtimeId",
                principalTable: "Showtimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Movies_MovieId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CartId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Showtimes",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "Showtimes",
                newName: "Showtime");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ShowtimeId",
                table: "Ticket",
                newName: "IX_Ticket_ShowtimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_MovieId",
                table: "Showtime",
                newName: "IX_Showtime_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showtime",
                table: "Showtime",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Showtime_Movies_MovieId",
                table: "Showtime",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Showtime_ShowtimeId",
                table: "Ticket",
                column: "ShowtimeId",
                principalTable: "Showtime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
