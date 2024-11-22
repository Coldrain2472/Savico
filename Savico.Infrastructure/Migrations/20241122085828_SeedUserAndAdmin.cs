using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Savico.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserAndAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BudgetId", "ConcurrencyStamp", "Currency", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "164abdf9-c8ce-43fb-93e3-53e78cd89369", 0, 1, "febaf27a-f002-4882-9c9d-db6c7edb70d3", "EUR", "testuser123@gmail.com", false, "Test", false, "User", false, null, "TESTUSER123@GMAIL.COM", "TESTUSER", "AQAAAAIAAYagAAAAEMhgqviHlAvu4WKLiBnq0vOwtuloZPpTzNlNyzXKu8zcHWsQbDczNYJZFysdoz2NEQ==", null, false, "2952ed3d-feb3-4e11-aee8-fea63c2a9118", false, "TestUser" },
                    { "89b0fffd-ea3f-4d8e-b2f3-85b80ab84693", 0, 2, "0ce264ec-e884-4bd0-b552-7d6bd89d37c6", "USD", "admin@admin.com", false, "TEST", false, "ADMIN", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEGvIvaQF1eOiPZbg12VnTiQz483TvHbsjZbjF6Qpr+dXUysgiDtfLlgUSNH8/9xMlw==", null, false, "ac8e5be0-1056-41ee-bc57-3b96f6a0b6dc", false, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "164abdf9-c8ce-43fb-93e3-53e78cd89369");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89b0fffd-ea3f-4d8e-b2f3-85b80ab84693");
        }
    }
}
