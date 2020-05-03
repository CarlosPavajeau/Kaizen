using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class Services : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    ServiceTypeId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Services_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c3a2326-db72-4ccb-aa1a-a8a19b6ca19f", "c74155f6-049a-4df8-b0c3-cb7f0e017183", "Administrator", "ADMINISTRATOR" },
                    { "1b4eeed8-53ec-476f-8efe-df02b13ddeb6", "9a2851f7-fbea-4aaf-8ef5-d58d2c1dffa3", "OfficeEmployee", "OFFICEEMPLOYEE" },
                    { "b2be4b0d-d951-4c14-9480-36708f84ce35", "5ad22159-f60a-4305-9fc8-1b1b68751d15", "TechnicalEmployee", "TECHNICALEMPLOYEE" },
                    { "baa40f94-39cc-42e4-a241-aa38b250a893", "fb4241e1-adbb-4d9d-bab8-a4a0301d4d5f", "Client", "CLIENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b4eeed8-53ec-476f-8efe-df02b13ddeb6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c3a2326-db72-4ccb-aa1a-a8a19b6ca19f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2be4b0d-d951-4c14-9480-36708f84ce35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "baa40f94-39cc-42e4-a241-aa38b250a893");

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
    }
}
