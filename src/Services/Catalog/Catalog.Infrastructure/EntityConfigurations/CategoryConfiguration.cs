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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Name is required and has a max length of 100
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Description has a max length of 500
            builder.Property(c => c.Description)
                .HasMaxLength(500);

            // Level is required
            builder.Property(c => c.Level)
                .IsRequired();

            // IsActive is required
            builder.Property(c => c.IsActive)
                .IsRequired();

            // Configure the relationship for the ParentCategory
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            //
            // Shadow properties
            builder.Property(c => c.CreatedDate)
                .IsRequired();

            builder.Property(c => c.LastModifiedDate)
                .IsRequired();

            builder.Property<string>("CreatedBy")
                .HasMaxLength(100)
                .HasDefaultValue("");

            builder.Property<string>("LastModifiedBy")
                .HasMaxLength(100)
                .HasDefaultValue("");

            builder.HasData(
              new Category("Elektronik", "Elektronik ürünler", null, 1) { Id = 1 },
              new Category("Bilgisayar", "Bilgisayar ve aksesuarlar", 1, 2) { Id = 2 },
              new Category("Laptop", "Dizüstü bilgisayarlar", 2, 3) { Id = 3 },
              new Category("Masaüstü", "Masaüstü bilgisayarlar", 2, 3) { Id = 4 },
              new Category("Telefon", "Cep telefonları", 1, 2) { Id = 5 }
          );
        }
    }
}
