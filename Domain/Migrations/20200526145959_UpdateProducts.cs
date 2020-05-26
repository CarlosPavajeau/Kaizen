using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class UpdateProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSheet");

            migrationBuilder.DropTable(
                name: "EmergencyCard");

            migrationBuilder.DropTable(
                name: "SafetySheet");

            migrationBuilder.AlterColumn<string>(
                name: "HealthRegister",
                table: "Products",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30) CHARACTER SET utf8mb4",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSheet",
                table: "Products",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyCard",
                table: "Products",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SafetySheet",
                table: "Products",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSheet",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EmergencyCard",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SafetySheet",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "HealthRegister",
                table: "Products",
                type: "varchar(30) CHARACTER SET utf8mb4",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DataSheet",
                columns: table => new
                {
                    ProductCode = table.Column<string>(type: "varchar(15) CHARACTER SET utf8mb4", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "varchar(350) CHARACTER SET utf8mb4", maxLength: 350, nullable: true),
                    Presentation = table.Column<string>(type: "varchar(40) CHARACTER SET utf8mb4", maxLength: 40, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSheet", x => x.ProductCode);
                    table.ForeignKey(
                        name: "FK_DataSheet_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyCard",
                columns: table => new
                {
                    ProductCode = table.Column<string>(type: "varchar(15) CHARACTER SET utf8mb4", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyCard", x => x.ProductCode);
                    table.ForeignKey(
                        name: "FK_EmergencyCard_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetySheet",
                columns: table => new
                {
                    ProductCode = table.Column<string>(type: "varchar(15) CHARACTER SET utf8mb4", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetySheet", x => x.ProductCode);
                    table.ForeignKey(
                        name: "FK_SafetySheet_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
