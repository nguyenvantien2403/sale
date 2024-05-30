using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sale.Domain.Migrations
{
    /// <inheritdoc />
    public partial class altertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1cbb6b5a-3e05-4af2-b809-05da9e6c4ce6"), "2", "User", "User" },
                    { new Guid("448cc461-f32a-42b7-af5e-5f6adb90784c"), "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("1cbb6b5a-3e05-4af2-b809-05da9e6c4ce6"));

            migrationBuilder.DeleteData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("448cc461-f32a-42b7-af5e-5f6adb90784c"));
        }
    }
}
