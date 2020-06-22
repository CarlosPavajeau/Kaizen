using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class UpdateEmployeeCharges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeCharges",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "EmployeeCharges",
                keyColumn: "Id",
                keyValue: 6,
                column: "Charge",
                value: "Técnico Operativo");

            migrationBuilder.UpdateData(
                table: "EmployeeCharges",
                keyColumn: "Id",
                keyValue: 7,
                column: "Charge",
                value: "Aprendiz");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployeeCharges",
                keyColumn: "Id",
                keyValue: 6,
                column: "Charge",
                value: "Técnico Operativo Lider");

            migrationBuilder.UpdateData(
                table: "EmployeeCharges",
                keyColumn: "Id",
                keyValue: 7,
                column: "Charge",
                value: "Técnico Operativo");

            migrationBuilder.InsertData(
                table: "EmployeeCharges",
                columns: new[] { "Id", "Charge" },
                values: new object[] { 8, "Aprendiz" });
        }
    }
}
