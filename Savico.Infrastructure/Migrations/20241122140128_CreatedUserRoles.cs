using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Savico.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9365c92c-dcb7-4c90-aa4c-c3460905eea0", null, "User", "USER" },
                    { "f2aaa3b1-3cd0-4384-b843-0c022bbfcf63", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "635a870b-c535-4877-8b8c-bd6d7604baa6", "AQAAAAIAAYagAAAAEMaEXaSrdvX6etcIU9d11spjXZKOx3LsGKmEhghZUaixdnFLIcLGbb9s4UOExiOZ3Q==", "c55af11f-df8f-41b0-9656-53764b65a067" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c5f8861-6361-484e-b17c-8de711a89425", "AQAAAAIAAYagAAAAEImq0g/SN6mdOGsq8Q5+VIjDyQibN8wraanHYCxfc9QrsDld4Bj8gGIcyB/oXeLrqg==", "11e74f6e-e236-443b-aa1a-0a12ec1dfcc1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f2aaa3b1-3cd0-4384-b843-0c022bbfcf63", "7c55600b-d374-4bfb-8352-a65bf7d44cc1" },
                    { "9365c92c-dcb7-4c90-aa4c-c3460905eea0", "ad5a5ccb-2be1-4866-b151-0f09a58416f6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUserRole<Guid>");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2aaa3b1-3cd0-4384-b843-0c022bbfcf63", "7c55600b-d374-4bfb-8352-a65bf7d44cc1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9365c92c-dcb7-4c90-aa4c-c3460905eea0", "ad5a5ccb-2be1-4866-b151-0f09a58416f6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9365c92c-dcb7-4c90-aa4c-c3460905eea0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2aaa3b1-3cd0-4384-b843-0c022bbfcf63");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "220bbfb2-d20e-4f9d-90b9-ab26db2dcfee", "AQAAAAIAAYagAAAAENuBmAloFASTCIXCNeyfyFWRxoZIh3OqHSyWiXQwjYOD19mN2xsZUCkWIN6EXQyDRQ==", "c243c0c8-8f66-4764-9510-a7fe403c6510" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ae11e18-b982-411d-b8e1-a247a4428011", "AQAAAAIAAYagAAAAENii02Fu+d8Qg4EYpPY8OoJjonETJyZw+mpgbnWo2u8XpDHZwNUgpeffBvPXm3o9sw==", "42fc174b-6f98-427c-a38f-25e3fcdbc2fd" });
        }
    }
}
