using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityPointRH.Data.Migrations
{
    /// <inheritdoc />
    public partial class scaffoldCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentHire",
                columns: table => new
                {
                    EquipmentHireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentHire", x => x.EquipmentHireId);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenuesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenuesId);
                });

            migrationBuilder.CreateTable(
                name: "VenueBookings",
                columns: table => new
                {
                    VenueBookingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenuesId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueBookings", x => x.VenueBookingsId);
                    table.ForeignKey(
                        name: "FK_VenueBookings_Venues_VenuesId",
                        column: x => x.VenuesId,
                        principalTable: "Venues",
                        principalColumn: "VenuesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueEquipment",
                columns: table => new
                {
                    VenueEquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueId = table.Column<int>(type: "int", nullable: false),
                    EquipmentHireId = table.Column<int>(type: "int", nullable: false),
                    IsIncluded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueEquipment", x => x.VenueEquipmentId);
                    table.ForeignKey(
                        name: "FK_VenueEquipment_EquipmentHire_EquipmentHireId",
                        column: x => x.EquipmentHireId,
                        principalTable: "EquipmentHire",
                        principalColumn: "EquipmentHireId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenueEquipment_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenuesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentBookings",
                columns: table => new
                {
                    EquipmentBookingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueBookingsId = table.Column<int>(type: "int", nullable: false),
                    EquipmentHireId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentBookings", x => x.EquipmentBookingsId);
                    table.ForeignKey(
                        name: "FK_EquipmentBookings_EquipmentHire_EquipmentHireId",
                        column: x => x.EquipmentHireId,
                        principalTable: "EquipmentHire",
                        principalColumn: "EquipmentHireId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentBookings_VenueBookings_VenueBookingsId",
                        column: x => x.VenueBookingsId,
                        principalTable: "VenueBookings",
                        principalColumn: "VenueBookingsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentBookings_EquipmentHireId",
                table: "EquipmentBookings",
                column: "EquipmentHireId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentBookings_VenueBookingsId",
                table: "EquipmentBookings",
                column: "VenueBookingsId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueBookings_VenuesId",
                table: "VenueBookings",
                column: "VenuesId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueEquipment_EquipmentHireId",
                table: "VenueEquipment",
                column: "EquipmentHireId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueEquipment_VenueId",
                table: "VenueEquipment",
                column: "VenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentBookings");

            migrationBuilder.DropTable(
                name: "VenueEquipment");

            migrationBuilder.DropTable(
                name: "VenueBookings");

            migrationBuilder.DropTable(
                name: "EquipmentHire");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
