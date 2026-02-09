using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityPointRH.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToVenueBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "VenueBookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "VenueBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_VenueBookings_UserId",
                table: "VenueBookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VenueBookings_AspNetUsers_UserId",
                table: "VenueBookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VenueBookings_AspNetUsers_UserId",
                table: "VenueBookings");

            migrationBuilder.DropIndex(
                name: "IX_VenueBookings_UserId",
                table: "VenueBookings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VenueBookings");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "VenueBookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
