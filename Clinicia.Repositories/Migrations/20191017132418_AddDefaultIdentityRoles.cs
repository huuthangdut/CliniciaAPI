using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinicia.Repositories.Migrations
{
    public partial class AddDefaultIdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { "9fddb8df-f0c6-4f07-9044-ba397fee2442", "SuperAdmin", "SUPERADMIN" }
            );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { "a7a2b51c-2d4f-4cac-a93d-7f7fa9644613", "PracticeAdmin", "PRACTICEADMIN" }
            );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { "3693ea35-968b-4cc8-a530-1529c78c844f", "Doctor", "DOCTOR" }
            );

            migrationBuilder.InsertData(
              table: "Roles",
              columns: new[] { "Id", "Name", "NormalizedName" },
              values: new object[] { "9a456ca3-77a4-49c2-b253-fa67145eb4eb", "Patient", "PATIENT" }
          );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "Roles",
               keyColumn: "Id",
               keyValue: "9fddb8df-f0c6-4f07-9044-ba397fee2442"
           );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a7a2b51c-2d4f-4cac-a93d-7f7fa9644613"
            );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3693ea35-968b-4cc8-a530-1529c78c844f"
            );

            migrationBuilder.DeleteData(
               table: "Roles",
               keyColumn: "Id",
               keyValue: "9a456ca3-77a4-49c2-b253-fa67145eb4eb"
           );
        }
    }
}