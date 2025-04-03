using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    ProductAttributes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "LastModifiedBy", "LastModifiedDate", "Logo", "Name" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Apple Inc.", true, "", null, "/images/brands/apple.png", "Apple" },
                    { 2, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Samsung Electronics Co., Ltd.", true, "", null, "/images/brands/samsung.png", "Samsung" },
                    { 3, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Hewlett-Packard Company", true, "", null, "/images/brands/hp.png", "HP" },
                    { 4, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Lenovo Group Limited", true, "", null, "/images/brands/lenovo.png", "Lenovo" },
                    { 5, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Dell Technologies Inc.", true, "", null, "/images/brands/dell.png", "Dell" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "LastModifiedBy", "LastModifiedDate", "Level", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Elektronik ürünler", true, "", null, 1, "Elektronik", null },
                    { 2, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Bilgisayar ve aksesuarlar", true, "", null, 2, "Bilgisayar", 1 },
                    { 5, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Cep telefonları", true, "", null, 2, "Telefon", 1 },
                    { 3, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Dizüstü bilgisayarlar", true, "", null, 3, "Laptop", 2 },
                    { 4, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Masaüstü bilgisayarlar", true, "", null, 3, "Masaüstü", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "Name", "Price", "ProductAttributes", "SKU", "Status", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, 5, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Apple Iphone 12", "", null, "Iphone 12", 10000m, "[]", "IP12", "Active", 100 },
                    { 2, 1, 5, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Apple Iphone 11", "", null, "Iphone 11", 8000m, "[]", "IP11", "Active", 100 },
                    { 3, 2, 5, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Samsung Galaxy S21", "", null, "Samsung S21", 9000m, "[]", "S21", "Active", 100 },
                    { 4, 2, 5, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Samsung Galaxy S20", "", null, "Samsung S20", 7000m, "[]", "S20", "Active", 100 },
                    { 5, 3, 3, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "HP Pavilion Laptop", "", null, "HP Pavilion", 6000m, "[]", "HP", "Active", 100 },
                    { 6, 4, 3, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Lenovo Thinkpad Laptop", "", null, "Lenovo Thinkpad", 7000m, "[]", "Lenovo", "Active", 100 },
                    { 7, 5, 3, "", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Dell XPS Laptop", "", null, "Dell XPS", 8000m, "[]", "Dell", "Active", 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
