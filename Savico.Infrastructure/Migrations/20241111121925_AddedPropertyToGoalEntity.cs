using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savico.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertyToGoalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastContributionDate",
                table: "Goals",
                type: "datetime2",
                nullable: true,
                comment: "Last date that a contribution was made on");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastContributionDate",
                table: "Goals");
        }
    }
}
