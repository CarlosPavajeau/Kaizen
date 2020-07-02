using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class RenameServiceTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Desinfección de ambientes y superficies");

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Captura y reubicación de animales");

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Matenimiento de sistemas y equipos");

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Jardinería");

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Suministro, instalación y mantenimiento de equipos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Saneamiento");

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Limpieza de espacios");

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Lavado y desinfección de tanques de agua");

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Captura y rehabilidación de animales domesticos");

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Jardineria");
        }
    }
}
