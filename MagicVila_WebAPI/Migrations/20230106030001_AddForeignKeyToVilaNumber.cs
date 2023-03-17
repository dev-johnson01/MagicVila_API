using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVilaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VilaID",
                table: "VilaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 6, 4, 0, 1, 601, DateTimeKind.Local).AddTicks(6150));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 6, 4, 0, 1, 601, DateTimeKind.Local).AddTicks(6168));

            migrationBuilder.CreateIndex(
                name: "IX_VilaNumbers_VilaID",
                table: "VilaNumbers",
                column: "VilaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VilaNumbers_Vilas_VilaID",
                table: "VilaNumbers",
                column: "VilaID",
                principalTable: "Vilas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VilaNumbers_Vilas_VilaID",
                table: "VilaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VilaNumbers_VilaID",
                table: "VilaNumbers");

            migrationBuilder.DropColumn(
                name: "VilaID",
                table: "VilaNumbers");

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 12, 5, 13, 25, 29, 172, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 12, 5, 13, 25, 29, 172, DateTimeKind.Local).AddTicks(3254));
        }
    }
}
