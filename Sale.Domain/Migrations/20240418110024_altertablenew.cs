using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sale.Domain.Migrations
{
    /// <inheritdoc />
    public partial class altertablenew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "TotalCount",
                table: "Orders",
                newName: "totalPrice");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Orders",
                newName: "orderNotes");

            migrationBuilder.RenameColumn(
                name: "Quanlity",
                table: "Carts",
                newName: "count");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mobile",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "firstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "mobile",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Orders",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "totalPrice",
                table: "Orders",
                newName: "TotalCount");

            migrationBuilder.RenameColumn(
                name: "orderNotes",
                table: "Orders",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "count",
                table: "Carts",
                newName: "Quanlity");

            migrationBuilder.AddColumn<double>(
                name: "Shipping",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
