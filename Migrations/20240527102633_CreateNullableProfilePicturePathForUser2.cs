using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddyAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateNullableProfilePicturePathForUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
