using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteCodeApi.Migrations
{
    /// <inheritdoc />
    public partial class NoteUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteUsers_Users_UserId",
                table: "NoteUsers");

            migrationBuilder.DropIndex(
                name: "IX_NoteUsers_UserId",
                table: "NoteUsers");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "NoteUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoteUsers_UsersId",
                table: "NoteUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteUsers_Users_UsersId",
                table: "NoteUsers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteUsers_Users_UsersId",
                table: "NoteUsers");

            migrationBuilder.DropIndex(
                name: "IX_NoteUsers_UsersId",
                table: "NoteUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "NoteUsers");

            migrationBuilder.CreateIndex(
                name: "IX_NoteUsers_UserId",
                table: "NoteUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteUsers_Users_UserId",
                table: "NoteUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
