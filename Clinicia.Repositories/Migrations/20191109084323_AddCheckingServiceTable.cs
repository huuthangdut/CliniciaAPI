using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinicia.Repositories.Migrations
{
    public partial class AddCheckingServiceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Appointments",
                newName: "TotalMinutes");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Appointments",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Appointments",
                newName: "Note");

            migrationBuilder.CreateTable(
                name: "CheckingServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    DurationInMinutes = table.Column<int>(nullable: false),
                    PriceFrom = table.Column<decimal>(nullable: true),
                    PriceTo = table.Column<decimal>(nullable: true),
                    DoctorId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUser = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckingServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckingServices_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false),
                    CheckingServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentDetails_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentDetails_CheckingServices_CheckingServiceId",
                        column: x => x.CheckingServiceId,
                        principalTable: "CheckingServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetails_AppointmentId",
                table: "AppointmentDetails",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetails_CheckingServiceId",
                table: "AppointmentDetails",
                column: "CheckingServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckingServices_DoctorId",
                table: "CheckingServices",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDetails");

            migrationBuilder.DropTable(
                name: "CheckingServices");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Appointments",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TotalMinutes",
                table: "Appointments",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Appointments",
                newName: "Description");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
