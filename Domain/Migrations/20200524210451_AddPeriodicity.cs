using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class AddPeriodicity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Periodicity",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: 8);

            migrationBuilder.AddColumn<int>(
                name: "Periodicity",
                table: "Activity",
                nullable: false,
                defaultValue: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Periodicity",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "Periodicity",
                table: "Activity");
        }
    }
}
