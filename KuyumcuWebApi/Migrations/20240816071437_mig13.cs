using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuyumcuWebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_products_productId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderStatuses",
                table: "orderStatuses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "orders");

            migrationBuilder.RenameTable(
                name: "orderStatuses",
                newName: "orderStatus");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "orders",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_productId",
                table: "orders",
                newName: "IX_orders_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderStatus",
                table: "orderStatus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_OrderStatusId",
                table: "orders",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_orderStatus_OrderStatusId",
                table: "orders",
                column: "OrderStatusId",
                principalTable: "orderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_products_ProductId",
                table: "orders",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_orderStatus_OrderStatusId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_products_ProductId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_OrderStatusId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderStatus",
                table: "orderStatus");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "orders");

            migrationBuilder.RenameTable(
                name: "orderStatus",
                newName: "orderStatuses");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "orders",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_ProductId",
                table: "orders",
                newName: "IX_orders_productId");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderStatuses",
                table: "orderStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_products_productId",
                table: "orders",
                column: "productId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
