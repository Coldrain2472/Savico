using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Savico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35bb4e58-cc3b-418d-8e6f-7b27594dc913", "AQAAAAIAAYagAAAAEMuEl97P63qfTaa6RbojB4xevE33UHgm7MqUvHBJfkqtKxaYfv0sWVBNz9xrQIz+5A==", "06cecd91-e57c-43d2-9669-ff7734c5d7c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4a07b8e-fc4a-4b3b-9a48-1331363e19c6", "AQAAAAIAAYagAAAAEFUCjmMrfspY8lSBatM2CVpoFXkgfg9wmw6vKRRWTqiImUKWFm+l8G+k+JDAAJ4kUQ==", "0302f72c-0a75-46b9-80fa-86c8058a9a47" });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "EndDate", "IsDeleted", "StartDate", "TotalAmount", "UserId" },
                values: new object[] { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000000.00m, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "BudgetId", "CategoryId", "Date", "Description", "IsDeleted", "UserId" },
                values: new object[,]
                {
                    { 14, 80m, 1, 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electricity bill", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 15, 100m, 1, 3, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Supermarket shopping", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 16, 5m, 1, 10, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amazon Prime monthly subscription", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 17, 10m, 1, 10, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netflix monthly subscription", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 18, 13m, 1, 10, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "World of Warcraft monthly subscription", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 19, 70m, 1, 11, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 20, 35m, 1, 13, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new pair of jeans", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 21, 50m, 1, 12, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Donated to a local animal shelter", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 22, 40m, 1, 4, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new video game from Steam", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 23, 50m, 1, 9, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Went to check my teeth", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 24, 30m, 1, 1, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water bill", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 25, 90m, 1, 13, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new pair of sneakers", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 26, 27m, 1, 10, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Disney+ monthly subscription", false, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "ContributionAmount", "CurrentAmount", "Description", "IsAchieved", "IsDeleted", "LastContributionDate", "TargetAmount", "TargetDate", "UserId" },
                values: new object[,]
                {
                    { 3, 0m, 0m, "Saving for my trip to Denmark", false, false, null, 1500m, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 4, 0m, 0m, "Saving for my PC upgrade", false, false, null, 500m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7c55600b-d374-4bfb-8352-a65bf7d44cc1" }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "Id", "Amount", "BudgetId", "Date", "IsDeleted", "Source", "UserId" },
                values: new object[,]
                {
                    { 4, 2350m, null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Salary", "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 5, 150m, null, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Freelance", "7c55600b-d374-4bfb-8352-a65bf7d44cc1" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "EndDate", "IsDeleted", "StartDate", "TotalExpense", "TotalIncome", "UserId" },
                values: new object[,]
                {
                    { 3, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0m, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 600m, 2500m, "7c55600b-d374-4bfb-8352-a65bf7d44cc1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Budgets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 4);

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
        }
    }
}
