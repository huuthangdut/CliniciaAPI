using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinicia.Repositories.Migrations
{
    public partial class AddDeviceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "DeviceUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeviceUuid = table.Column<string>(nullable: false),
                    DeviceType = table.Column<string>(nullable: false),
                    DeviceToken = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ExpiredAt = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUser = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsers_UserId",
                table: "DeviceUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }
    }
}
