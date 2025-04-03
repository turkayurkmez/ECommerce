﻿// <auto-generated />
using System;
using ECommerce.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerce.Catalog.Infrastructure.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Apple Inc.",
                            IsActive = true,
                            LastModifiedBy = "",
                            Logo = "/images/brands/apple.png",
                            Name = "Apple"
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Samsung Electronics Co., Ltd.",
                            IsActive = true,
                            LastModifiedBy = "",
                            Logo = "/images/brands/samsung.png",
                            Name = "Samsung"
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Hewlett-Packard Company",
                            IsActive = true,
                            LastModifiedBy = "",
                            Logo = "/images/brands/hp.png",
                            Name = "HP"
                        },
                        new
                        {
                            Id = 4,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Lenovo Group Limited",
                            IsActive = true,
                            LastModifiedBy = "",
                            Logo = "/images/brands/lenovo.png",
                            Name = "Lenovo"
                        },
                        new
                        {
                            Id = 5,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Dell Technologies Inc.",
                            IsActive = true,
                            LastModifiedBy = "",
                            Logo = "/images/brands/dell.png",
                            Name = "Dell"
                        });
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Elektronik ürünler",
                            IsActive = true,
                            LastModifiedBy = "",
                            Level = 1,
                            Name = "Elektronik"
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Bilgisayar ve aksesuarlar",
                            IsActive = true,
                            LastModifiedBy = "",
                            Level = 2,
                            Name = "Bilgisayar",
                            ParentCategoryId = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Dizüstü bilgisayarlar",
                            IsActive = true,
                            LastModifiedBy = "",
                            Level = 3,
                            Name = "Laptop",
                            ParentCategoryId = 2
                        },
                        new
                        {
                            Id = 4,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Masaüstü bilgisayarlar",
                            IsActive = true,
                            LastModifiedBy = "",
                            Level = 3,
                            Name = "Masaüstü",
                            ParentCategoryId = 2
                        },
                        new
                        {
                            Id = 5,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Cep telefonları",
                            IsActive = true,
                            LastModifiedBy = "",
                            Level = 2,
                            Name = "Telefon",
                            ParentCategoryId = 1
                        });
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("LastModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductAttributes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            CategoryId = 5,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Apple Iphone 12",
                            LastModifiedBy = "",
                            Name = "Iphone 12",
                            Price = 10000m,
                            ProductAttributes = "[]",
                            SKU = "IP12",
                            Status = "Active",
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 1,
                            CategoryId = 5,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Apple Iphone 11",
                            LastModifiedBy = "",
                            Name = "Iphone 11",
                            Price = 8000m,
                            ProductAttributes = "[]",
                            SKU = "IP11",
                            Status = "Active",
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 2,
                            CategoryId = 5,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Samsung Galaxy S21",
                            LastModifiedBy = "",
                            Name = "Samsung S21",
                            Price = 9000m,
                            ProductAttributes = "[]",
                            SKU = "S21",
                            Status = "Active",
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 2,
                            CategoryId = 5,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Samsung Galaxy S20",
                            LastModifiedBy = "",
                            Name = "Samsung S20",
                            Price = 7000m,
                            ProductAttributes = "[]",
                            SKU = "S20",
                            Status = "Active",
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 5,
                            BrandId = 3,
                            CategoryId = 3,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "HP Pavilion Laptop",
                            LastModifiedBy = "",
                            Name = "HP Pavilion",
                            Price = 6000m,
                            ProductAttributes = "[]",
                            SKU = "HP",
                            Status = "Active",
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 6,
                            BrandId = 4,
                            CategoryId = 3,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Lenovo Thinkpad Laptop",
                            LastModifiedBy = "",
                            Name = "Lenovo Thinkpad",
                            Price = 7000m,
                            ProductAttributes = "[]",
                            SKU = "Lenovo",
                            Status = "Active",
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 7,
                            BrandId = 5,
                            CategoryId = 3,
                            CreatedBy = "",
                            CreatedDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Dell XPS Laptop",
                            LastModifiedBy = "",
                            Name = "Dell XPS",
                            Price = 8000m,
                            ProductAttributes = "[]",
                            SKU = "Dell",
                            Status = "Active",
                            StockQuantity = 100
                        });
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.Category", b =>
                {
                    b.HasOne("ECommerce.Catalog.Domain.Entities.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.Product", b =>
                {
                    b.HasOne("ECommerce.Catalog.Domain.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ECommerce.Catalog.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.ProductImage", b =>
                {
                    b.HasOne("ECommerce.Catalog.Domain.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("ECommerce.Catalog.Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductImages");
                });
#pragma warning restore 612, 618
        }
    }
}
