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
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(pi => pi.ImageUrl)
              .IsRequired()
              .HasMaxLength(500);

            builder.Property(pi => pi.IsMain)
                .IsRequired();

            builder.Property(pi => pi.SortOrder)
                .IsRequired();

            // İlişkiler
            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Shadow properties
            builder.Property(pi => pi.CreatedDate)
                .IsRequired();

            builder.Property(pi => pi.LastModifiedDate)
                .IsRequired();
        }
    }
}
