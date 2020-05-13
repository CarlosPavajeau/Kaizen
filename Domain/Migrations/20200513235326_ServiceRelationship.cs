using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class ServiceRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeesServices",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(maxLength: 10, nullable: false),
                    ServiceCode = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesServices", x => new { x.EmployeeId, x.ServiceCode });
                    table.ForeignKey(
                        name: "FK_EmployeesServices_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesServices_Services_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "Services",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentsServices",
                columns: table => new
                {
                    EquipmentCode = table.Column<string>(maxLength: 20, nullable: false),
                    ServiceCode = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentsServices", x => new { x.EquipmentCode, x.ServiceCode });
                    table.ForeignKey(
                        name: "FK_EquipmentsServices_Equipments_EquipmentCode",
                        column: x => x.EquipmentCode,
                        principalTable: "Equipments",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentsServices_Services_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "Services",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsServices",
                columns: table => new
                {
                    ProductCode = table.Column<string>(maxLength: 15, nullable: false),
                    ServiceCode = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsServices", x => new { x.ServiceCode, x.ProductCode });
                    table.ForeignKey(
                        name: "FK_ProductsServices_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsServices_Services_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "Services",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesServices_ServiceCode",
                table: "EmployeesServices",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentsServices_ServiceCode",
                table: "EquipmentsServices",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsServices_ProductCode",
                table: "ProductsServices",
                column: "ProductCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesServices");

            migrationBuilder.DropTable(
                name: "EquipmentsServices");

            migrationBuilder.DropTable(
                name: "ProductsServices");
        }
    }
}
