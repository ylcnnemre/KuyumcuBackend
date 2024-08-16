using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuyumcuWebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "roles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "productImages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "productImages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "orderStatus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "orderStatus",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "addresses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "addresses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 16, 7, 34, 37, 466, DateTimeKind.Utc).AddTicks(698), null });

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 16, 7, 34, 37, 466, DateTimeKind.Utc).AddTicks(700), null });

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 16, 7, 34, 37, 466, DateTimeKind.Utc).AddTicks(702), null });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 16, 7, 34, 37, 465, DateTimeKind.Utc).AddTicks(8859), null });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 16, 7, 34, 37, 465, DateTimeKind.Utc).AddTicks(8862), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "productImages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "productImages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "orderStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "orderStatus");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "addresses");
        }
    }
}
