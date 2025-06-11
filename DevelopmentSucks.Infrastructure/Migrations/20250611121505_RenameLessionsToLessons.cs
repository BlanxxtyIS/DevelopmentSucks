using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevelopmentSucks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameLessionsToLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessions_Chapters_ChapterId",
                table: "Lessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessions",
                table: "Lessions");

            migrationBuilder.RenameTable(
                name: "Lessions",
                newName: "Lessons");

            migrationBuilder.RenameIndex(
                name: "IX_Lessions_ChapterId",
                table: "Lessons",
                newName: "IX_Lessons_ChapterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Chapters_ChapterId",
                table: "Lessons",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Chapters_ChapterId",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "Lessions");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_ChapterId",
                table: "Lessions",
                newName: "IX_Lessions_ChapterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessions",
                table: "Lessions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessions_Chapters_ChapterId",
                table: "Lessions",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
