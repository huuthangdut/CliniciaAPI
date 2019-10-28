using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinicia.Repositories.Migrations
{
    public partial class UpdateLocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Locations",
                newName: "FormattedAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FormattedAddress",
                table: "Locations",
                newName: "Zip");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Locations",
                nullable: true);
        }
    }
}
