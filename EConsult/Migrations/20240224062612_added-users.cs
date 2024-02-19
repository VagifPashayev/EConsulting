using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EConsult.Migrations
{
    public partial class addedusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersItems");

            migrationBuilder.RenameColumn(
                name: "TrackingCode",
                table: "Orders",
                newName: "LobbyCode");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProductId",
                table: "Users",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Products_ProductId",
                table: "Users",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Products_ProductId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProductId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "LobbyCode",
                table: "Orders",
                newName: "TrackingCode");

            migrationBuilder.CreateTable(
                name: "OrdersItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductColorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductOrderPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductSizeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItems_OrderId",
                table: "OrdersItems",
                column: "OrderId");
        }
    }
}
