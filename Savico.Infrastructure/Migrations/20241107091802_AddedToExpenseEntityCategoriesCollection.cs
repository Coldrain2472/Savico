using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savico.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedToExpenseEntityCategoriesCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "ExpenseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "ExpenseId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ExpenseId",
                table: "Categories",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Expenses_ExpenseId",
                table: "Categories",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Expenses_ExpenseId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ExpenseId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => new { x.CategoryId, x.ExpenseId });
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_ExpenseId",
                table: "ExpenseCategories",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_UserId",
                table: "ExpenseCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
