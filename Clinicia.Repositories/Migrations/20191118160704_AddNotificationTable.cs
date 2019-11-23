using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinicia.Repositories.Migrations
{
    public partial class AddNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    NotificationDate = table.Column<DateTime>(nullable: false),
                    HasRead = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUser = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
