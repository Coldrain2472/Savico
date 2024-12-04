using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Savico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "187d17be-8508-4a9d-a9cd-e964249307a2", "AQAAAAIAAYagAAAAEAlGD/UoRqZTaWYA/uYmFG9sPsXD2IMGmrRIPE56hrL/uU3z3796iNi7b+SPtT2NrA==", "896df298-29f7-470f-8efa-8e3bc2a495e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85e27472-e031-4b42-9f93-f22a6d75b65d", "AQAAAAIAAYagAAAAEIrhIt6ilPPRbjHGzGPucCD5qZ3PeK+uTzQn65XKvqGF8WDzynhRDywUPVn6DcP3Qg==", "3ab17732-0e19-42c3-a34f-937187fb13c7" });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "EndDate", "IsDeleted", "StartDate", "TotalAmount", "UserId" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000000.00m, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "ContributionAmount", "CurrentAmount", "Description", "IsAchieved", "IsDeleted", "LastContributionDate", "TargetAmount", "TargetDate", "UserId" },
                values: new object[,]
                {
                    { 1, 0m, 0m, "Saving for my trip to Denmark", false, false, null, 1500m, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 2, 0m, 0m, "Saving for my PC upgrade", false, false, null, 500m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ad5a5ccb-2be1-4866-b151-0f09a58416f6" }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "Id", "Amount", "BudgetId", "Date", "IsDeleted", "Source", "UserId" },
                values: new object[,]
                {
                    { 1, 2700m, null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Salary", "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 2, 200m, null, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Freelance", "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 3, 100m, null, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Investments", "ad5a5ccb-2be1-4866-b151-0f09a58416f6" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "EndDate", "IsDeleted", "StartDate", "TotalExpense", "TotalIncome", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0m, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 700m, 3000m, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "BudgetId", "CategoryId", "Date", "Description", "IsDeleted", "UserId" },
                values: new object[,]
                {
                    { 1, 100m, 1, 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electricity bill", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 2, 150m, 1, 3, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Supermarket shopping", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 3, 5m, 1, 10, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amazon Prime monthly subscription", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 4, 10m, 1, 10, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netflix monthly subscription", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 5, 13m, 1, 10, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "World of Warcraft monthly subscription", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 6, 100m, 1, 11, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 7, 35m, 1, 13, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new pair of jeans", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 8, 50m, 1, 12, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Donated to a local animal shelter", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 9, 40m, 1, 4, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new video game from Steam", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 10, 50m, 1, 9, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Went to check my teeth", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 11, 30m, 1, 1, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water bill", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 12, 100m, 1, 13, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new pair of sneakers", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" },
                    { 13, 17m, 1, 10, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Disney+ monthly subscription", false, "ad5a5ccb-2be1-4866-b151-0f09a58416f6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a7cd0a2-22f3-4ff8-b7e3-c9cb2c03a418", "AQAAAAIAAYagAAAAENCglzgtFIkK5WVfkXQqy4mt8VtpiuUVw6xP3xPGsdB7xujXEhd0ylB3634zDB2Ncw==", "aea18c3e-f1d0-48fa-b52e-6b271fc5e02d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a4c34ec-fbe3-4335-9698-2bdb70fc145e", "AQAAAAIAAYagAAAAEOCX7bzZ2eNU9Jck/rvMmaSSUgitKKHpLiotws5mbAkmkxFWZHsekpmDodZF2UCy6A==", "df76974a-6606-4010-b911-fd8de5b3c64b" });
        }
    }
}
