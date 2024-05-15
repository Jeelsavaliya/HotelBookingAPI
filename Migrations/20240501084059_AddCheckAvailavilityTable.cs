using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckAvailavilityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckAvailabilitys",
                columns: table => new
                {
                    CheckAvailabilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckAvailabilitys", x => x.CheckAvailabilityID);
                    table.ForeignKey(
                        name: "FK_CheckAvailabilitys_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckAvailabilitys_RoomID",
                table: "CheckAvailabilitys",
                column: "RoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckAvailabilitys");
        }
    }
}
