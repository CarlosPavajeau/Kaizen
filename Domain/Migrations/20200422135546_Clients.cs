using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class Clients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    SecondName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    SeconLastname = table.Column<string>(maxLength: 20, nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ClientType = table.Column<string>(maxLength: 20, nullable: true),
                    BusninessName = table.Column<string>(maxLength: 50, nullable: true),
                    TradeName = table.Column<string>(maxLength: 50, nullable: true),
                    FirstPhoneNumber = table.Column<string>(maxLength: 10, nullable: false),
                    SecondPhoneNumber = table.Column<string>(maxLength: 10, nullable: true),
                    FirstLandLine = table.Column<string>(maxLength: 15, nullable: true),
                    SecondLandLine = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientAddresses",
                columns: table => new
                {
                    ClientId = table.Column<string>(maxLength: 10, nullable: false),
                    City = table.Column<string>(maxLength: 40, nullable: true),
                    Neighborhood = table.Column<string>(maxLength: 40, nullable: true),
                    Street = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAddresses", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_ClientAddresses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPeople",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 10, nullable: true),
                    ClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPeople_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPeople_ClientId",
                table: "ContactPeople",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientAddresses");

            migrationBuilder.DropTable(
                name: "ContactPeople");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
