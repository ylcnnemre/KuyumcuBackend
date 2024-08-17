using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuyumcuWebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 35, 3, 98, DateTimeKind.Utc).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 35, 3, 98, DateTimeKind.Utc).AddTicks(2398));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 35, 3, 98, DateTimeKind.Utc).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 35, 3, 98, DateTimeKind.Utc).AddTicks(304));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 35, 3, 98, DateTimeKind.Utc).AddTicks(307));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 28, 49, 861, DateTimeKind.Utc).AddTicks(5932));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 28, 49, 861, DateTimeKind.Utc).AddTicks(5934));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 28, 49, 861, DateTimeKind.Utc).AddTicks(5935));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 28, 49, 861, DateTimeKind.Utc).AddTicks(3913));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 12, 28, 49, 861, DateTimeKind.Utc).AddTicks(3919));
        }
    }
}
