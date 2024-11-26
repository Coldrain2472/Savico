using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedGoalProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyContribution",
                table: "Goals");

            migrationBuilder.AddColumn<decimal>(
                name: "ContributionAmount",
                table: "Goals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                comment: "Contribution towards the set goal");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "3a7cd0a2-22f3-4ff8-b7e3-c9cb2c03a418", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAENCglzgtFIkK5WVfkXQqy4mt8VtpiuUVw6xP3xPGsdB7xujXEhd0ylB3634zDB2Ncw==", "aea18c3e-f1d0-48fa-b52e-6b271fc5e02d", "admin@admin.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9a4c34ec-fbe3-4335-9698-2bdb70fc145e", "TESTUSER123@GMAIL.COM", "AQAAAAIAAYagAAAAEOCX7bzZ2eNU9Jck/rvMmaSSUgitKKHpLiotws5mbAkmkxFWZHsekpmDodZF2UCy6A==", "df76974a-6606-4010-b911-fd8de5b3c64b", "testuser123@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContributionAmount",
                table: "Goals");

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyContribution",
                table: "Goals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                comment: "Monthly contribution towards the set goal");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "635a870b-c535-4877-8b8c-bd6d7604baa6", "ADMIN", "AQAAAAIAAYagAAAAEMaEXaSrdvX6etcIU9d11spjXZKOx3LsGKmEhghZUaixdnFLIcLGbb9s4UOExiOZ3Q==", "c55af11f-df8f-41b0-9656-53764b65a067", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "8c5f8861-6361-484e-b17c-8de711a89425", "TESTUSER", "AQAAAAIAAYagAAAAEImq0g/SN6mdOGsq8Q5+VIjDyQibN8wraanHYCxfc9QrsDld4Bj8gGIcyB/oXeLrqg==", "11e74f6e-e236-443b-aa1a-0a12ec1dfcc1", "TestUser" });
        }
    }
}
