using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class ClientSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientSignature",
                table: "WorkOrders",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientSignature",
                table: "WorkOrders");
        }
    }
}
