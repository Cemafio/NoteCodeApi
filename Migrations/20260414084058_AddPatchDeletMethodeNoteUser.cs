using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteCodeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPatchDeletMethodeNoteUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "NoteUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "NoteUsers");
        }
    }
}
