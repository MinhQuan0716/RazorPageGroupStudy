using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infracstuructures.Migrations
{
    /// <inheritdoc />
    public partial class fixcommentcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_ReplyToCommentId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ReplyToCommentId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReplyToCommentId",
                table: "Comments",
                column: "ReplyToCommentId",
                unique: true,
                filter: "[ReplyToCommentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_ReplyToCommentId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ReplyToCommentId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReplyToCommentId",
                table: "Comments",
                column: "ReplyToCommentId",
                unique: true);
        }
    }
}
