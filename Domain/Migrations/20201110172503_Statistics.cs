using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class Statistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YearStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppliedActivities = table.Column<int>(nullable: false),
                    ClientsVisited = table.Column<int>(nullable: false),
                    ClientsRegistered = table.Column<int>(nullable: false),
                    Profits = table.Column<decimal>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearStatistics", x => x.Id);
                    table.UniqueConstraint("AK_YearStatistics_Year", x => x.Year);
                });

            migrationBuilder.CreateTable(
                name: "MonthStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppliedActivities = table.Column<int>(nullable: false),
                    ClientsVisited = table.Column<int>(nullable: false),
                    ClientsRegistered = table.Column<int>(nullable: false),
                    Profits = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    YearStatisticsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthStatistics_YearStatistics_YearStatisticsId",
                        column: x => x.YearStatisticsId,
                        principalTable: "YearStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppliedActivities = table.Column<int>(nullable: false),
                    ClientsVisited = table.Column<int>(nullable: false),
                    ClientsRegistered = table.Column<int>(nullable: false),
                    Profits = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    MonthStatisticsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayStatistics_MonthStatistics_MonthStatisticsId",
                        column: x => x.MonthStatisticsId,
                        principalTable: "MonthStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayStatistics_MonthStatisticsId",
                table: "DayStatistics",
                column: "MonthStatisticsId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthStatistics_YearStatisticsId",
                table: "MonthStatistics",
                column: "YearStatisticsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayStatistics");

            migrationBuilder.DropTable(
                name: "MonthStatistics");

            migrationBuilder.DropTable(
                name: "YearStatistics");
        }
    }
}
