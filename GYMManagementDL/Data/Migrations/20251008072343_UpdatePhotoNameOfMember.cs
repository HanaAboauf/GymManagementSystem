using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYMManagementDL.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhotoNameOfMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "photo",
                table: "Members",
                newName: "Photo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Members",
                newName: "photo");
        }
    }
}
