using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityPointRoomHire.Data.Migrations
{
    /// <inheritdoc />
    public partial class surecreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipment",
                table: "Venues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Equipment",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
