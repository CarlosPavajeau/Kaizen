using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class UpdateEquipmentsAndProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Equipments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Equipments",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Equipments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DataSheet",
                maxLength: 350,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Presentation",
                table: "DataSheet",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "DataSheet",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DataSheet");

            migrationBuilder.DropColumn(
                name: "Presentation",
                table: "DataSheet");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "DataSheet");
        }
    }
}
