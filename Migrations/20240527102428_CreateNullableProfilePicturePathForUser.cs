using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddyAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateNullableProfilePicturePathForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Trips");
        }
    }
}
