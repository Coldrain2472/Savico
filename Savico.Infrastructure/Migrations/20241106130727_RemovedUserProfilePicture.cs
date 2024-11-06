using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savico.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserProfilePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                comment: "User's profile picture");
        }
    }
}
