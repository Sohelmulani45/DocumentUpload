using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentUploadApp.Migrations
{
    /// <inheritdoc />
    public partial class StoreFileInDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Employees",
                newName: "ContentType");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "Employees",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "Employees",
                newName: "FilePath");
        }
    }
}
