using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinicia.Repositories.Migrations
{
    public partial class AddSendNotificationBeforeMinutesToAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PushNotificationEnabled",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnseenNotificationCount",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SendNotificationBeforeMinutes",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PushNotificationEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnseenNotificationCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SendNotificationBeforeMinutes",
                table: "Appointments");
        }
    }
}
