using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EConsult.Migrations
{
    public partial class deletecart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductColor");

            migrationBuilder.DropTable(
                name: "ProductSize");

            migrationBuilder.DropTable(
                name: "SlideBanners");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -4,
                column: "Name",
                value: "Sport");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3,
                column: "Name",
                value: "Agro");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2,
                column: "Name",
                value: "Business");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1,
                column: "Name",
                value: "IT");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "IsConfirmed", "LastName", "Name", "Password" },
                values: new object[] { true, "Usta", "Yaver", "$2a$11$Ku17sT3/epVd63/Ptx9lNO6zQcfX6McHLRa0Uj7isuhZvnLztkZmO" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SlideBanners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Offer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    RedirectionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideBanners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductColor",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColor", x => new { x.ProductId, x.ColorId });
                    table.ForeignKey(
                        name: "FK_ProductColor_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColor_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSize",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSize", x => new { x.ProductId, x.SizeId });
                    table.ForeignKey(
                        name: "FK_ProductSize_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSize_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -4,
                column: "Name",
                value: "Classic sport");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3,
                column: "Name",
                value: "Sport");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2,
                column: "Name",
                value: "Fruts");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1,
                column: "Name",
                value: "Laundry");

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { -6, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Black", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -5, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Yellow", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -4, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Purple", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -3, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Green", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -2, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Red", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -1, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Blue", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { -6, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "XXL", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -5, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "XL", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -4, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "L", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -3, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "XS", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -2, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "S", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -1, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "X", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "IsConfirmed", "LastName", "Name", "Password" },
                values: new object[] { false, "Heyder", "Eshqin", "$2a$11$xCZqgx6GkO/46MSfciD/te2KysN8pO37Eb0aUyZpebAsAhDbz" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ColorId",
                table: "ProductColor",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_SizeId",
                table: "ProductSize",
                column: "SizeId");
        }
    }
}
