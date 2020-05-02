using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class EmployeesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeCharges_EmployeeChargeId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeChargeId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "234e4485-2c2f-41b8-97bf-9e9490bcf57f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d0851ab-2854-4237-a965-6ee69af6a412");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45b16ba7-15a6-47a4-a45c-4124b6ed5730");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fd8f3dc-a0b8-49a9-861e-f7966ba7ec2e");

            migrationBuilder.DropColumn(
                name: "EmployeeChargeId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "ChargeId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "68460e2f-be95-4451-815c-76b2fc1fea39", "69f255df-16c8-45ff-ad6c-1dfdea188470", "Administrator", "ADMINISTRATOR" },
                    { "080b86e9-1143-43e8-b143-0b3e65718bde", "b4818380-f9bb-4c81-a53f-31930748dd00", "OfficeEmployee", "OFFICEEMPLOYEE" },
                    { "23f3e8bc-bc93-43ad-83f1-b0039d945bef", "97260604-cb15-403b-9a93-3e208e1cc498", "TechnicalEmployee", "TECHNICALEMPLOYEE" },
                    { "a7da5b51-d324-46ac-bed9-f1ad06cd0c74", "f522019a-81db-4f37-9fdb-8d4ba6aabf8f", "Client", "CLIENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ChargeId",
                table: "Employees",
                column: "ChargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeCharges_ChargeId",
                table: "Employees",
                column: "ChargeId",
                principalTable: "EmployeeCharges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeCharges_ChargeId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ChargeId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "080b86e9-1143-43e8-b143-0b3e65718bde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23f3e8bc-bc93-43ad-83f1-b0039d945bef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68460e2f-be95-4451-815c-76b2fc1fea39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7da5b51-d324-46ac-bed9-f1ad06cd0c74");

            migrationBuilder.DropColumn(
                name: "ChargeId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeChargeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "234e4485-2c2f-41b8-97bf-9e9490bcf57f", "50f64641-20c2-4da2-a11e-005f5af85538", "Administrator", "ADMINISTRATOR" },
                    { "45b16ba7-15a6-47a4-a45c-4124b6ed5730", "f8f53e67-50dd-4a79-b370-896666a78a5b", "OfficeEmployee", "OFFICEEMPLOYEE" },
                    { "5fd8f3dc-a0b8-49a9-861e-f7966ba7ec2e", "ea5fbbe6-67ab-4aa8-9d9b-6592beed89a8", "TechnicalEmployee", "TECHNICALEMPLOYEE" },
                    { "3d0851ab-2854-4237-a965-6ee69af6a412", "7f1cc9e4-c41b-4815-83a3-9881e7cf596d", "Client", "CLIENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeChargeId",
                table: "Employees",
                column: "EmployeeChargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeCharges_EmployeeChargeId",
                table: "Employees",
                column: "EmployeeChargeId",
                principalTable: "EmployeeCharges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
