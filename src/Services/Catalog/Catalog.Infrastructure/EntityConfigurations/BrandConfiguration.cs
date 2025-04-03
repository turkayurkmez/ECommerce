using ECommerce.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .IsRequired();

            builder.Property<string>("CreatedBy")
                .HasMaxLength(100)
                .HasDefaultValue("");

            builder.Property<string>("LastModifiedBy")
                .HasMaxLength(100)
                .HasDefaultValue("");

            // Seed Data - Example brands
            builder.HasData(
                new Brand("Apple", "Apple Inc.", "/images/brands/apple.png") { Id = 1 },
                new Brand("Samsung", "Samsung Electronics Co., Ltd.", "/images/brands/samsung.png") { Id = 2 },
                new Brand("HP", "Hewlett-Packard Company", "/images/brands/hp.png") { Id = 3 },
                new Brand("Lenovo", "Lenovo Group Limited", "/images/brands/lenovo.png") { Id = 4 },
                new Brand("Dell", "Dell Technologies Inc.", "/images/brands/dell.png") { Id = 5 }
            );

        }
    }
}
