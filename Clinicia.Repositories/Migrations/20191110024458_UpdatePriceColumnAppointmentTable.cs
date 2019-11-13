using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinicia.Repositories.Migrations
{
    public partial class UpdatePriceColumnAppointmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDetails");

            migrationBuilder.DropColumn(
                name: "PriceFrom",
                table: "CheckingServices");

            migrationBuilder.DropColumn(
                name: "PriceTo",
                table: "CheckingServices");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CheckingServices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CheckingServiceId",
                table: "Appointments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CheckingServiceId",
                table: "Appointments",
                column: "CheckingServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_CheckingServices_CheckingServiceId",
                table: "Appointments",
                column: "CheckingServiceId",
                principalTable: "CheckingServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_CheckingServices_CheckingServiceId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CheckingServiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CheckingServices");

            migrationBuilder.DropColumn(
                name: "CheckingServiceId",
                table: "Appointments");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceFrom",
                table: "CheckingServices",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceTo",
                table: "CheckingServices",
                nullable: true);

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
        }
    }
}
