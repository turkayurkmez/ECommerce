using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ECommerce.Catalog.Infrastructure.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {


            //identity column
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.StockQuantity).HasDefaultValue(0);
            builder.Property(x => x.SKU).HasMaxLength(50)
                                        .IsRequired()
                                        .HasMaxLength(50);

            builder.Property(x => x.Status).IsRequired()
                             .HasConversion<string>();

            builder.HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(x => x.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasMany(x => x.ProductImages)
                .WithOne()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.Property(p => p.ProductAttributes)
                   .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                                  v => v == null ? new List<ProductAttribute>()
                                                 : JsonSerializer.Deserialize<List<ProductAttribute>>(v, (JsonSerializerOptions)null),
            new ValueComparer<IReadOnlyCollection<ProductAttribute>>(
            // Determines equality
            (c1, c2) => c1.SequenceEqual(c2),
            // Generates hash code for collection
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            // Creates a snapshot for change tracking
            c => c.ToList()
        ));

            //Shadow properties:
            var createdDate = new DateTime(2025, 4, 3, 0, 0, 0, DateTimeKind.Local);

            //builder.Property(p => p.CreatedDate).HasDefaultValue(createdDate);

            builder.Property(p => p.LastModifiedDate).IsRequired(false);

            builder.Property<string>("CreatedBy").HasMaxLength(100)
                                                 .HasDefaultValue("");

            builder.Property<string>("LastModifiedBy").HasMaxLength(100).HasDefaultValue("");

            //seed data: Her bir kategori ve marka için birer ürün oluşturulacak.

         

            builder.HasData(
                new Product("Iphone 12", "Apple Iphone 12", 10000, 100, "IP12", 5, 1) { Id = 1, CreatedDate = createdDate },
                new Product("Iphone 11", "Apple Iphone 11", 8000, 100, "IP11", 5, 1) { Id = 2, CreatedDate = createdDate },
                new Product("Samsung S21", "Samsung Galaxy S21", 9000, 100, "S21", 5, 2) { Id = 3, CreatedDate = createdDate },
                new Product("Samsung S20", "Samsung Galaxy S20", 7000, 100, "S20", 5, 2) { Id = 4, CreatedDate = createdDate },
                new Product("HP Pavilion", "HP Pavilion Laptop", 6000, 100, "HP", 3, 3) { Id = 5, CreatedDate = createdDate },
                new Product("Lenovo Thinkpad", "Lenovo Thinkpad Laptop", 7000, 100, "Lenovo", 3, 4) { Id = 6, CreatedDate= createdDate },
                new Product("Dell XPS", "Dell XPS Laptop", 8000, 100, "Dell", 3, 5) { Id = 7, CreatedDate = createdDate }
            );




        }
    }
}
