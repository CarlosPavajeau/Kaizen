using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class UpdateClientsAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02720c03-9688-42ad-bd35-5d376febfd5f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bc8ac2f-7009-4c35-8462-a9376610ea52");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a6705c7-28ae-44b2-a4e9-75940b80a202");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0ff9bc5-46f8-42ed-b1bd-cd02c375b3b5");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetRoles",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30) CHARACTER SET utf8mb4",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30) CHARACTER SET utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "fdc80d00-b870-4c3b-af4d-bb3efa4ecc5d", "ba2a7b98-7642-4796-be72-eeb2a950a2e0", "Administrator", null },
                    { "539a49e6-e4a4-4901-bd7e-7a1a45e55538", "7764d8f7-d1b9-407a-b6c5-c3dc74fa0eeb", "OfficeEmployee", null },
                    { "e3150991-efa0-4e6a-a9a2-f37a56d0e2e3", "a421481b-25ff-4dcf-a3b4-15215e87ab4a", "TechnicalEmployee", null },
                    { "28742ef0-462a-4067-b4a6-28b700f03c92", "78eada3b-c0c5-4540-a72f-ce15523a39ba", "Client", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28742ef0-462a-4067-b4a6-28b700f03c92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "539a49e6-e4a4-4901-bd7e-7a1a45e55538");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3150991-efa0-4e6a-a9a2-f37a56d0e2e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdc80d00-b870-4c3b-af4d-bb3efa4ecc5d");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "varchar(30) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetRoles",
                type: "varchar(30) CHARACTER SET utf8mb4",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "varchar(30) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b0ff9bc5-46f8-42ed-b1bd-cd02c375b3b5", "969b6286-40d4-4495-8805-1b2663505619", "Administrator", null },
                    { "6a6705c7-28ae-44b2-a4e9-75940b80a202", "0ce30ddb-46d8-433d-856f-bcaa5f3eeddb", "OfficeEmployee", null },
                    { "02720c03-9688-42ad-bd35-5d376febfd5f", "e1fabf72-0cca-4ca5-b7dc-4f4735172871", "TechnicalEmployee", null },
                    { "1bc8ac2f-7009-4c35-8462-a9376610ea52", "487e81c1-b5a4-4b92-b8bd-1055bb71814f", "Client", null }
                });
        }
    }
}
