using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class IdentityRolesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a46bd475-60d2-47cd-832a-52df574d53f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b615bc82-255d-4566-a6c4-51831d00aaf8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c79cfe46-42a9-4822-b6a5-acdccabfdc0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "feeaf3c5-c1f6-45a2-9d2a-f72bba7fc5b3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "234e4485-2c2f-41b8-97bf-9e9490bcf57f", "50f64641-20c2-4da2-a11e-005f5af85538", "Administrator", "ADMINISTRATOR" },
                    { "45b16ba7-15a6-47a4-a45c-4124b6ed5730", "f8f53e67-50dd-4a79-b370-896666a78a5b", "OfficeEmployee", "OFFICEEMPLOYEE" },
                    { "5fd8f3dc-a0b8-49a9-861e-f7966ba7ec2e", "ea5fbbe6-67ab-4aa8-9d9b-6592beed89a8", "TechnicalEmployee", "TECHNICALEMPLOYEE" },
                    { "3d0851ab-2854-4237-a965-6ee69af6a412", "7f1cc9e4-c41b-4815-83a3-9881e7cf596d", "Client", "CLIENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "234e4485-2c2f-41b8-97bf-9e9490bcf57f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d0851ab-2854-4237-a965-6ee69af6a412");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45b16ba7-15a6-47a4-a45c-4124b6ed5730");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fd8f3dc-a0b8-49a9-861e-f7966ba7ec2e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "feeaf3c5-c1f6-45a2-9d2a-f72bba7fc5b3", "479d83b6-8150-4229-8696-b23d9dd06a38", "Administrator", null },
                    { "a46bd475-60d2-47cd-832a-52df574d53f5", "a7010682-7bfe-4764-9366-2a61e56b2854", "OfficeEmployee", null },
                    { "c79cfe46-42a9-4822-b6a5-acdccabfdc0d", "26f6af68-effb-4e00-861e-a2bd935805d4", "TechnicalEmployee", null },
                    { "b615bc82-255d-4566-a6c4-51831d00aaf8", "7abf7b4a-006e-405e-8bee-21ef0c974c59", "Client", null }
                });
        }
    }
}
