using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savico.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewBoolPropertyToTheGoalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAchieved",
                table: "Goals",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates if the goal is achieved or not");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAchieved",
                table: "Goals");
        }
    }
}
