using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class EquipmentsAndProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    MaintenanceDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    HealthRegister = table.Column<string>(maxLength: 30, nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    ApplicationMonths = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "DataSheet",
                columns: table => new
                {
                    ProductCode = table.Column<string>(maxLength: 15, nullable: false)
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
                    ProductCode = table.Column<string>(maxLength: 15, nullable: false)
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
                    ProductCode = table.Column<string>(maxLength: 15, nullable: false)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "298dd05e-fc70-4f85-8053-e826fe29b8e0", "ed5d2489-3712-4be6-9eb5-e0db13778837", "Administrator", "ADMINISTRATOR" },
                    { "74abbc13-e025-4b74-88e6-84b8ec48139f", "ea1f4f22-ea1a-4482-b35b-0af538a9743e", "OfficeEmployee", "OFFICEEMPLOYEE" },
                    { "93c317f9-efb1-48c0-83f8-39854bb52845", "59a32f4e-a5c9-4f5f-bdbe-880ac8221816", "TechnicalEmployee", "TECHNICALEMPLOYEE" },
                    { "571f9f40-3e0f-4fe5-bb5b-b1e52898b545", "79cbe650-8d2f-4529-a90b-3476f9ec95ba", "Client", "CLIENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSheet");

            migrationBuilder.DropTable(
                name: "EmergencyCard");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "SafetySheet");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "298dd05e-fc70-4f85-8053-e826fe29b8e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "571f9f40-3e0f-4fe5-bb5b-b1e52898b545");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74abbc13-e025-4b74-88e6-84b8ec48139f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c317f9-efb1-48c0-83f8-39854bb52845");

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
        }
    }
}
