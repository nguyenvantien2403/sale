using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sale.Domain.Migrations
{
    /// <inheritdoc />
    public partial class altertabbleComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Comments",
                newName: "comment");

            migrationBuilder.AlterColumn<string>(
                name: "comment",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userPost",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "userPost",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Comments",
                newName: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
