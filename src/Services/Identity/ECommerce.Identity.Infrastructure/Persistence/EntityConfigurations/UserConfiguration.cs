using ECommerce.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configure the User entity properties and relationships here

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            //unique index for username
            builder.HasIndex(u => u.UserName).IsUnique();


            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            //unique index for email
            builder.HasIndex(u => u.Email).IsUnique();

            //first name and last name are optional and max length 50
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);

            //isActive required and default value is true
            builder.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);

            //email confirmed required and default value is false
            builder.Property(u => u.EmailConfirmed).IsRequired().HasDefaultValue(false);

            builder.Property(u => u.PasswordHash).IsRequired();

            //navigation properties
            builder.HasMany(u => u.Roles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.RefreshTokens)
                   .WithOne(rt => rt.User)
                   .HasForeignKey(rt => rt.UserId)
                   .OnDelete(DeleteBehavior.Cascade);





            // Add other properties and configurations as needed
        }
    }

}
