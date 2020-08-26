using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class FixEmployeeContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeContract_ContractCode",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_UserId",
                table: "Employees");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Employees_ContractCode",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Employees",
                maxLength: 191,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(191) CHARACTER SET utf8mb4",
                oldMaxLength: 191,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractCode",
                table: "Employees",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30) CHARACTER SET utf8mb4",
                oldMaxLength: 30);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ContractCode",
                table: "Employees",
                column: "ContractCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeContract_ContractCode",
                table: "Employees",
                column: "ContractCode",
                principalTable: "EmployeeContract",
                principalColumn: "ContractCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeContract_ContractCode",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_UserId",
                table: "Employees");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ContractCode",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "varchar(191) CHARACTER SET utf8mb4",
                maxLength: 191,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 191);

            migrationBuilder.AlterColumn<string>(
                name: "ContractCode",
                table: "Employees",
                type: "varchar(30) CHARACTER SET utf8mb4",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Employees_ContractCode",
                table: "Employees",
                column: "ContractCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeContract_ContractCode",
                table: "Employees",
                column: "ContractCode",
                principalTable: "EmployeeContract",
                principalColumn: "ContractCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
