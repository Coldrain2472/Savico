using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedUserAndAdminProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "459d1f00-ae18-4c9b-b3a8-05463765540f", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEAl4W3NNtM5rUpN0AH4h/n/0TsgwNlX8e7jJZaMcsTyHyG8tWCw5UWyDjyxsqht4Zg==", "10b39758-0173-4613-8dbb-123b6a94c902", "admin@admin.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9825532b-41d0-4879-a284-4e8ea4d1bc34", "TESTUSER123@GMAIL.COM", "AQAAAAIAAYagAAAAEGh5u/nayJQgKaRLHj2ZNKPd8eRpxpVZWqTYd5hPJ4nUzWN5XbBFg6RkRKStEQ2N/A==", "bc2adc1c-5a4d-46bd-9f12-59f4f84d8a34", "testuser123@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
