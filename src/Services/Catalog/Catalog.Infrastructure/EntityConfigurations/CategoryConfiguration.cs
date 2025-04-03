using ECommerce.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .IsRequired(false);

            builder.Property<string>("CreatedBy")
                .HasMaxLength(100)
                .HasDefaultValue("");

            builder.Property<string>("LastModifiedBy")
                .HasMaxLength(100)
                .HasDefaultValue("");

            var createdDate = new DateTime(2025, 4, 3,0,0,0, DateTimeKind.Local);


            builder.HasData(
              new Category("Elektronik", "Elektronik ürünler", null, 1) { Id = 1, CreatedDate = createdDate },
              new Category("Bilgisayar", "Bilgisayar ve aksesuarlar", 1, 2) { Id = 2, CreatedDate = createdDate },
              new Category("Laptop", "Dizüstü bilgisayarlar", 2, 3) { Id = 3 , CreatedDate = createdDate },
              new Category("Masaüstü", "Masaüstü bilgisayarlar", 2, 3) { Id = 4 , CreatedDate = createdDate },
              new Category("Telefon", "Cep telefonları", 1, 2) { Id = 5 , CreatedDate = createdDate }
          );
        }
    }
}
