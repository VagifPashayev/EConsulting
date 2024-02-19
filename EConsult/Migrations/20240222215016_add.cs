using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EConsult.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhysicalImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Offer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideBanners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProducts", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "AlertMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertMessage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TrackingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActivations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductOrderPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductSizeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductColorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { -4, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Classic sport", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -3, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Sport", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -2, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Fruts", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -1, new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Laundry", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsConfirmed", "LastName", "Name", "Password", "Role", "UpdatedAt" },
                values: new object[] { -1, new DateTime(2023, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), "super_admin@gmail.com", false, "Heyder", "Eshqin", "$2a$11$xCZqgx6GkO/46MSfciD/te2KysN8pO37Eb0aUyZpebAsAhDbz", 4, new DateTime(2023, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_AlertMessage_UserId",
                table: "AlertMessage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProducts_CategoryId",
                table: "CategoryProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItems_OrderId",
                table: "OrdersItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ColorId",
                table: "ProductColor",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_SizeId",
                table: "ProductSize",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivations_UserId",
                table: "UserActivations",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertMessage");

            migrationBuilder.DropTable(
                name: "CategoryProducts");

            migrationBuilder.DropTable(
                name: "EmailMessages");

            migrationBuilder.DropTable(
                name: "OrdersItems");

            migrationBuilder.DropTable(
                name: "ProductColor");

            migrationBuilder.DropTable(
                name: "ProductSize");

            migrationBuilder.DropTable(
                name: "SlideBanners");

            migrationBuilder.DropTable(
                name: "UserActivations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
