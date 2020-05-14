using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class Activities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    ClientId = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Activity_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivitiesEmployees",
                columns: table => new
                {
                    ActivityCode = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitiesEmployees", x => new { x.EmployeeId, x.ActivityCode });
                    table.ForeignKey(
                        name: "FK_ActivitiesEmployees_Activity_ActivityCode",
                        column: x => x.ActivityCode,
                        principalTable: "Activity",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivitiesEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivitiesServices",
                columns: table => new
                {
                    ActivityCode = table.Column<int>(nullable: false),
                    ServiceCode = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitiesServices", x => new { x.ActivityCode, x.ServiceCode });
                    table.ForeignKey(
                        name: "FK_ActivitiesServices_Activity_ActivityCode",
                        column: x => x.ActivityCode,
                        principalTable: "Activity",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivitiesServices_Services_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "Services",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesEmployees_ActivityCode",
                table: "ActivitiesEmployees",
                column: "ActivityCode");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesServices_ServiceCode",
                table: "ActivitiesServices",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ClientId",
                table: "Activity",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivitiesEmployees");

            migrationBuilder.DropTable(
                name: "ActivitiesServices");

            migrationBuilder.DropTable(
                name: "Activity");
        }
    }
}
