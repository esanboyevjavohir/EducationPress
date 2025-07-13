using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPress.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreatedOn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c0ae7f44-f3a2-4ea6-8030-01a4ea1b1aee"),
                column: "CreatedOn",
                value: new DateTime(2025, 7, 13, 17, 11, 34, 591, DateTimeKind.Local).AddTicks(9188));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f67273d6-d1ee-4129-9740-75a8df1a5c5b"),
                column: "CreatedOn",
                value: new DateTime(2025, 7, 13, 17, 11, 34, 592, DateTimeKind.Local).AddTicks(1431));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c0ae7f44-f3a2-4ea6-8030-01a4ea1b1aee"),
                column: "CreatedOn",
                value: new DateTime(2025, 7, 13, 17, 9, 59, 562, DateTimeKind.Local).AddTicks(8493));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f67273d6-d1ee-4129-9740-75a8df1a5c5b"),
                column: "CreatedOn",
                value: new DateTime(2025, 7, 13, 12, 9, 59, 563, DateTimeKind.Utc).AddTicks(805));
        }
    }
}
