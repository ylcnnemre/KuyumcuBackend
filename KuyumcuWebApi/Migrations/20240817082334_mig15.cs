using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuyumcuWebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCanceled",
                table: "orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 8, 23, 33, 883, DateTimeKind.Utc).AddTicks(2648));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 8, 23, 33, 883, DateTimeKind.Utc).AddTicks(2652));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 8, 23, 33, 883, DateTimeKind.Utc).AddTicks(2653));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 8, 23, 33, 883, DateTimeKind.Utc).AddTicks(711));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 8, 23, 33, 883, DateTimeKind.Utc).AddTicks(716));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCanceled",
                table: "orders");

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 16, 7, 34, 37, 466, DateTimeKind.Utc).AddTicks(698));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 16, 7, 34, 37, 466, DateTimeKind.Utc).AddTicks(700));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 16, 7, 34, 37, 466, DateTimeKind.Utc).AddTicks(702));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 16, 7, 34, 37, 465, DateTimeKind.Utc).AddTicks(8859));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 16, 7, 34, 37, 465, DateTimeKind.Utc).AddTicks(8862));
        }
    }
}
