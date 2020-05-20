using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class UpdateEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractCode",
                table: "Employees",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Employees_ContractCode",
                table: "Employees",
                column: "ContractCode");

            migrationBuilder.CreateTable(
                name: "EmployeeContract",
                columns: table => new
                {
                    ContractCode = table.Column<string>(maxLength: 30, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContract", x => x.ContractCode);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeContract_ContractCode",
                table: "Employees",
                column: "ContractCode",
                principalTable: "EmployeeContract",
                principalColumn: "ContractCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeContract_ContractCode",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeContract");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Employees_ContractCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ContractCode",
                table: "Employees");
        }
    }
}
