using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuyumcuWebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCanceled",
                table: "orders");

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
                columns: new[] { "CreatedAt", "Type" },
                values: new object[] { new DateTime(2024, 8, 17, 12, 28, 49, 861, DateTimeKind.Utc).AddTicks(5935), "İptal" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "CreatedAt", "Type" },
                values: new object[] { new DateTime(2024, 8, 17, 8, 23, 33, 883, DateTimeKind.Utc).AddTicks(2653), "Reddedilen" });

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
    }
}
