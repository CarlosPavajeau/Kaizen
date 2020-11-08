using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class RemoveDefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Periodicity",
                table: "ServiceRequests",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 8);

            migrationBuilder.AlterColumn<int>(
                name: "Periodicity",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Periodicity",
                table: "ServiceRequests",
                type: "int",
                nullable: false,
                defaultValue: 8,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Periodicity",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 8,
                oldClrType: typeof(int));
        }
    }
}
