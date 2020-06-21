using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class AddSectors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sector",
                table: "WorkOrders");

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "WorkOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Industrial" },
                    { 2, "Comercial" },
                    { 3, "Alimentos" },
                    { 4, "Portuario" },
                    { 5, "Hotelero" },
                    { 6, "Salud" },
                    { 7, "Residencial" },
                    { 8, "Educativo" },
                    { 9, "Transporte" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_SectorId",
                table: "WorkOrders",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Sectors_SectorId",
                table: "WorkOrders",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Sectors_SectorId",
                table: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_SectorId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "WorkOrders");

            migrationBuilder.AddColumn<string>(
                name: "Sector",
                table: "WorkOrders",
                type: "varchar(40) CHARACTER SET utf8mb4",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
