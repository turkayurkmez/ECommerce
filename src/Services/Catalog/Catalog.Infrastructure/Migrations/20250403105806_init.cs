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
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId1 = table.Column<int>(type: "int", nullable: true),
                    CategoryId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        name: "FK_Products_Brands_BrandId1",
                        column: x => x.BrandId1,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Categories",
                        principalColumn: "Id");
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
                    { 1, "", new DateTime(2025, 4, 3, 10, 58, 5, 467, DateTimeKind.Utc).AddTicks(1472), "Apple Inc.", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/brands/apple.png", "Apple" },
                    { 2, "", new DateTime(2025, 4, 3, 10, 58, 5, 467, DateTimeKind.Utc).AddTicks(2814), "Samsung Electronics Co., Ltd.", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/brands/samsung.png", "Samsung" },
                    { 3, "", new DateTime(2025, 4, 3, 10, 58, 5, 467, DateTimeKind.Utc).AddTicks(2818), "Hewlett-Packard Company", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/brands/hp.png", "HP" },
                    { 4, "", new DateTime(2025, 4, 3, 10, 58, 5, 467, DateTimeKind.Utc).AddTicks(2819), "Lenovo Group Limited", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/brands/lenovo.png", "Lenovo" },
                    { 5, "", new DateTime(2025, 4, 3, 10, 58, 5, 467, DateTimeKind.Utc).AddTicks(2842), "Dell Technologies Inc.", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/brands/dell.png", "Dell" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "LastModifiedBy", "LastModifiedDate", "Level", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2025, 4, 3, 10, 58, 5, 473, DateTimeKind.Utc).AddTicks(3315), "Elektronik ürünler", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Elektronik", null },
                    { 2, "", new DateTime(2025, 4, 3, 10, 58, 5, 473, DateTimeKind.Utc).AddTicks(4759), "Bilgisayar ve aksesuarlar", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bilgisayar", 1 },
                    { 5, "", new DateTime(2025, 4, 3, 10, 58, 5, 473, DateTimeKind.Utc).AddTicks(4766), "Cep telefonları", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Telefon", 1 },
                    { 3, "", new DateTime(2025, 4, 3, 10, 58, 5, 473, DateTimeKind.Utc).AddTicks(4763), "Dizüstü bilgisayarlar", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Laptop", 2 },
                    { 4, "", new DateTime(2025, 4, 3, 10, 58, 5, 473, DateTimeKind.Utc).AddTicks(4765), "Masaüstü bilgisayarlar", true, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Masaüstü", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BrandId1", "CategoryId", "CategoryId1", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "Name", "Price", "ProductAttributes", "SKU", "Status", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, null, 5, null, "", new DateTime(2025, 4, 3, 10, 58, 5, 491, DateTimeKind.Utc).AddTicks(3924), "Apple Iphone 12", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iphone 12", 10000m, "[]", "IP12", "Active", 100 },
                    { 2, 1, null, 5, null, "", new DateTime(2025, 4, 3, 10, 58, 5, 491, DateTimeKind.Utc).AddTicks(8986), "Apple Iphone 11", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iphone 11", 8000m, "[]", "IP11", "Active", 100 },
                    { 3, 2, null, 5, null, "", new DateTime(2025, 4, 3, 10, 58, 5, 491, DateTimeKind.Utc).AddTicks(9003), "Samsung Galaxy S21", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung S21", 9000m, "[]", "S21", "Active", 100 },
                    { 4, 2, null, 5, null, "", new DateTime(2025, 4, 3, 10, 58, 5, 491, DateTimeKind.Utc).AddTicks(9007), "Samsung Galaxy S20", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung S20", 7000m, "[]", "S20", "Active", 100 },
                    { 5, 3, null, 3, null, "", new DateTime(2025, 4, 3, 10, 58, 5, 491, DateTimeKind.Utc).AddTicks(9009), "HP Pavilion Laptop", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HP Pavilion", 6000m, "[]", "HP", "Active", 100 },
                    { 6, 4, null, 3, null, "", new DateTime(2025, 4, 3, 10, 58, 5, 491, DateTimeKind.Utc).AddTicks(9012), "Lenovo Thinkpad Laptop", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lenovo Thinkpad", 7000m, "[]", "Lenovo", "Active", 100 },
                    { 7, 5, null, 3, null, "", new DateTime(2025, 4, 3, 10, 58, 5, 491, DateTimeKind.Utc).AddTicks(9016), "Dell XPS Laptop", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dell XPS", 8000m, "[]", "Dell", "Active", 100 }
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
                name: "IX_Products_BrandId1",
                table: "Products",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId1",
                table: "Products",
                column: "CategoryId1");
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
