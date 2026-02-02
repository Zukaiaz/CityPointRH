using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityPointRoomHire.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCapacityToVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                });

            migrationBuilder.CreateTable(
                name: "VenueEquipments",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueEquipments", x => new { x.VenueId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_VenueEquipments_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenueEquipments_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenuesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VenueEquipments_EquipmentId",
                table: "VenueEquipments",
                column: "EquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VenueEquipments");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Venues");
        }
    }
}
