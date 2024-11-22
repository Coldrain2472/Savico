using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Savico.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetUserAndAdminEmailConfirmedToTrue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "164abdf9-c8ce-43fb-93e3-53e78cd89369");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89b0fffd-ea3f-4d8e-b2f3-85b80ab84693");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BudgetId", "ConcurrencyStamp", "Currency", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7c55600b-d374-4bfb-8352-a65bf7d44cc1", 0, 2, "220bbfb2-d20e-4f9d-90b9-ab26db2dcfee", "USD", "admin@admin.com", true, "TEST", false, "ADMIN", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAENuBmAloFASTCIXCNeyfyFWRxoZIh3OqHSyWiXQwjYOD19mN2xsZUCkWIN6EXQyDRQ==", null, false, "c243c0c8-8f66-4764-9510-a7fe403c6510", false, "Admin" },
                    { "ad5a5ccb-2be1-4866-b151-0f09a58416f6", 0, 1, "1ae11e18-b982-411d-b8e1-a247a4428011", "EUR", "testuser123@gmail.com", true, "Test", false, "User", false, null, "TESTUSER123@GMAIL.COM", "TESTUSER", "AQAAAAIAAYagAAAAENii02Fu+d8Qg4EYpPY8OoJjonETJyZw+mpgbnWo2u8XpDHZwNUgpeffBvPXm3o9sw==", null, false, "42fc174b-6f98-427c-a38f-25e3fcdbc2fd", false, "TestUser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BudgetId", "ConcurrencyStamp", "Currency", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "164abdf9-c8ce-43fb-93e3-53e78cd89369", 0, 1, "febaf27a-f002-4882-9c9d-db6c7edb70d3", "EUR", "testuser123@gmail.com", false, "Test", false, "User", false, null, "TESTUSER123@GMAIL.COM", "TESTUSER", "AQAAAAIAAYagAAAAEMhgqviHlAvu4WKLiBnq0vOwtuloZPpTzNlNyzXKu8zcHWsQbDczNYJZFysdoz2NEQ==", null, false, "2952ed3d-feb3-4e11-aee8-fea63c2a9118", false, "TestUser" },
                    { "89b0fffd-ea3f-4d8e-b2f3-85b80ab84693", 0, 2, "0ce264ec-e884-4bd0-b552-7d6bd89d37c6", "USD", "admin@admin.com", false, "TEST", false, "ADMIN", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEGvIvaQF1eOiPZbg12VnTiQz483TvHbsjZbjF6Qpr+dXUysgiDtfLlgUSNH8/9xMlw==", null, false, "ac8e5be0-1056-41ee-bc57-3b96f6a0b6dc", false, "Admin" }
                });
        }
    }
}
