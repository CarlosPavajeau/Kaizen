using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class ServiceAndProdcutInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    IVA = table.Column<decimal>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    ClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInvoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    IVA = table.Column<decimal>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    ClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceInvoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    ProductInvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInvoiceDetails_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInvoiceDetails_ProductInvoices_ProductInvoiceId",
                        column: x => x.ProductInvoiceId,
                        principalTable: "ProductInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServiceCode = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    ServiceInvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceDetails_Services_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "Services",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceDetails_ServiceInvoices_ServiceInvoiceId",
                        column: x => x.ServiceInvoiceId,
                        principalTable: "ServiceInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoiceDetails_ProductCode",
                table: "ProductInvoiceDetails",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoiceDetails_ProductInvoiceId",
                table: "ProductInvoiceDetails",
                column: "ProductInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoices_ClientId",
                table: "ProductInvoices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceDetails_ServiceCode",
                table: "ServiceInvoiceDetails",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceDetails_ServiceInvoiceId",
                table: "ServiceInvoiceDetails",
                column: "ServiceInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoices_ClientId",
                table: "ServiceInvoices",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInvoiceDetails");

            migrationBuilder.DropTable(
                name: "ServiceInvoiceDetails");

            migrationBuilder.DropTable(
                name: "ProductInvoices");

            migrationBuilder.DropTable(
                name: "ServiceInvoices");
        }
    }
}
