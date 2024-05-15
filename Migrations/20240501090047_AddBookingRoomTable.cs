using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingRooms",
                columns: table => new
                {
                    BookingRoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRooms", x => x.BookingRoomID);
                    table.ForeignKey(
                        name: "FK_BookingRooms_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRooms_RoomID",
                table: "BookingRooms",
                column: "RoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingRooms");
        }
    }
}
