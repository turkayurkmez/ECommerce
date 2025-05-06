using ECommerce.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(256);

            //name unique index
            builder.HasIndex(r => r.Name).IsUnique();
            builder.Property(r => r.Description).HasMaxLength(200);

            var createdDate = new DateTime(2025, 5, 6, 12, 28, 57, 0, DateTimeKind.Local);

            //seed default roles
            builder.HasData(new Role[]
            {
                new Role("Admin", "Administrator role") { Id = Guid.Parse("c62471f6-7328-4b37-820c-ecff21dfa29f"), CreatedDate = createdDate  },
                new Role("User", "User role") { Id = Guid.Parse("e93ed64a-d0ca-41ad-8374-665058ef255d"), CreatedDate=createdDate }
            });


        }

    }

}
