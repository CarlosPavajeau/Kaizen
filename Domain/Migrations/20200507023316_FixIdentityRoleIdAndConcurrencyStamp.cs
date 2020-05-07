using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class FixIdentityRoleIdAndConcurrencyStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bb4b79d-85a4-4a94-b55e-5619c9acf4a2",
                column: "ConcurrencyStamp",
                value: "1ed77447-fe5c-42c2-9711-3f91cc103255");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a988a9ea-c7a5-4329-aceb-3da5016c6a43",
                column: "ConcurrencyStamp",
                value: "fba45aab-42d7-4e12-9dc0-44a2f68badf1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6728857-7423-443f-8228-2c8dd22f3aab",
                column: "ConcurrencyStamp",
                value: "501614ae-a5ad-4ee3-ba6f-17c28ab1cd5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e88f6181-e86a-49e1-a2da-c79c71914624",
                column: "ConcurrencyStamp",
                value: "177cda8b-1541-411e-8891-62f58b0e45fa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bb4b79d-85a4-4a94-b55e-5619c9acf4a2",
                column: "ConcurrencyStamp",
                value: "25e12472-bfa1-4879-899e-2bfce28fba48");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a988a9ea-c7a5-4329-aceb-3da5016c6a43",
                column: "ConcurrencyStamp",
                value: "de5866dc-4b6d-4cd3-a8c4-5c94f6adedee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6728857-7423-443f-8228-2c8dd22f3aab",
                column: "ConcurrencyStamp",
                value: "41a5d7b1-5f21-42e1-9c56-d749f4849975");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e88f6181-e86a-49e1-a2da-c79c71914624",
                column: "ConcurrencyStamp",
                value: "2a1b8e1f-5f62-42a2-ae2b-7040a0aaf997");
        }
    }
}
