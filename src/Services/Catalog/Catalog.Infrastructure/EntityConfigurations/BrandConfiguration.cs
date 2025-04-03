using ECommerce.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Catalog.Infrastructure.EntityConfigurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(b => b.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(b => b.Description)
                .HasMaxLength(1000);

            builder.Property(b => b.Logo)
                .HasMaxLength(500);

            builder.Property(b => b.IsActive)
                .IsRequired();

            // Shadow properties
            builder.Property(b => b.CreatedDate)
                .IsRequired();

            builder.Property(b => b.LastModifiedDate)
                .IsRequired(false);

            builder.Property<string>("CreatedBy")
                .HasMaxLength(100)
                .HasDefaultValue("");

            builder.Property<string>("LastModifiedBy")
                .HasMaxLength(100)
                .HasDefaultValue("");

            var createdDate = new DateTime(2025, 4, 3,0,0,0,DateTimeKind.Local);

            // Seed Data - Example brands
            builder.HasData(
                new Brand("Apple", "Apple Inc.", "/images/brands/apple.png") { Id = 1, CreatedDate = createdDate },
                new Brand("Samsung", "Samsung Electronics Co., Ltd.", "/images/brands/samsung.png") { Id = 2 , CreatedDate = createdDate },
                new Brand("HP", "Hewlett-Packard Company", "/images/brands/hp.png") { Id = 3 , CreatedDate = createdDate },
                new Brand("Lenovo", "Lenovo Group Limited", "/images/brands/lenovo.png") { Id = 4 , CreatedDate = createdDate },
                new Brand("Dell", "Dell Technologies Inc.", "/images/brands/dell.png") { Id = 5 , CreatedDate = createdDate }
            );

        }
    }
}
