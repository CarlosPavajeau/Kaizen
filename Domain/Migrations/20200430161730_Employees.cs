using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class Employees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "EmployeeCharges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Charge = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCharges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    SecondName = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    SecondLastName = table.Column<string>(maxLength: 20, nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    EmployeeChargeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeCharges_EmployeeChargeId",
                        column: x => x.EmployeeChargeId,
                        principalTable: "EmployeeCharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "EmployeeCharges",
                columns: new[] { "Id", "Charge" },
                values: new object[,]
                {
                    { 1, "Gerente" },
                    { 2, "Coordinador de Calidad y Ambiente" },
                    { 3, "Contador" },
                    { 4, "Lider SST" },
                    { 5, "Auxiliar Administrativa" },
                    { 6, "Técnico Operativo Lider" },
                    { 7, "Técnico Operativo" },
                    { 8, "Aprendiz" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeChargeId",
                table: "Employees",
                column: "EmployeeChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeCharges");

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
                    { "fdc80d00-b870-4c3b-af4d-bb3efa4ecc5d", "ba2a7b98-7642-4796-be72-eeb2a950a2e0", "Administrator", null },
                    { "539a49e6-e4a4-4901-bd7e-7a1a45e55538", "7764d8f7-d1b9-407a-b6c5-c3dc74fa0eeb", "OfficeEmployee", null },
                    { "e3150991-efa0-4e6a-a9a2-f37a56d0e2e3", "a421481b-25ff-4dcf-a3b4-15215e87ab4a", "TechnicalEmployee", null },
                    { "28742ef0-462a-4067-b4a6-28b700f03c92", "78eada3b-c0c5-4540-a72f-ce15523a39ba", "Client", null }
                });
        }
    }
}
