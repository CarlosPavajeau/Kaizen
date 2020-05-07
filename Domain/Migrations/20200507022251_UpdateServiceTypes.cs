using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaizen.Domain.Migrations
{
    public partial class UpdateServiceTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceTypes",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30) CHARACTER SET utf8mb4",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bb4b79d-85a4-4a94-b55e-5619c9acf4a2", "25e12472-bfa1-4879-899e-2bfce28fba48", "Administrator", "ADMINISTRATOR" },
                    { "e88f6181-e86a-49e1-a2da-c79c71914624", "2a1b8e1f-5f62-42a2-ae2b-7040a0aaf997", "OfficeEmployee", "OFFICEEMPLOYEE" },
                    { "e6728857-7423-443f-8228-2c8dd22f3aab", "41a5d7b1-5f21-42e1-9c56-d749f4849975", "TechnicalEmployee", "TECHNICALEMPLOYEE" },
                    { "a988a9ea-c7a5-4329-aceb-3da5016c6a43", "de5866dc-4b6d-4cd3-a8c4-5c94f6adedee", "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Control de plagas" },
                    { 2, "Saneamiento" },
                    { 3, "Limpieza de espacios" },
                    { 4, "Lavado y desinfección de tanques de agua" },
                    { 5, "Captura y rehabilidación de animales domesticos" },
                    { 6, "Jardineria" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bb4b79d-85a4-4a94-b55e-5619c9acf4a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a988a9ea-c7a5-4329-aceb-3da5016c6a43");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6728857-7423-443f-8228-2c8dd22f3aab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e88f6181-e86a-49e1-a2da-c79c71914624");

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceTypes",
                type: "varchar(30) CHARACTER SET utf8mb4",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 70,
                oldNullable: true);

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
        }
    }
}
