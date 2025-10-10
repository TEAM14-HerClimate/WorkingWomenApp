using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkingWomenApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class editprofileModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfChidren",
                table: "UserProfile",
                newName: "NumberOfChildren");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "UserProfile",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "NumberOfChildren",
                table: "UserProfile",
                newName: "NumberOfChidren");
        }
    }
}
