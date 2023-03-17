using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedTabble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vilas",
                columns: new[] { "Id", "Amaenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqrft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2022, 11, 26, 22, 41, 23, 158, DateTimeKind.Local).AddTicks(3270), "Have fun and enjoy yourself", "", "Villa pool", 4, 4.0, 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2022, 11, 26, 22, 41, 23, 158, DateTimeKind.Local).AddTicks(3283), "A place to explore the world", "", "Villa beach", 0, 5.0, 150, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
