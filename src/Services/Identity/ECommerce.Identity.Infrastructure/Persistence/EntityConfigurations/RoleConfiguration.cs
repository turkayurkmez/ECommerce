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

            //seed default roles
            builder.HasData(new Role[]
            {
                new Role("Admin", "Administrator role") { Id = Guid.NewGuid() },
                new Role("User", "User role") { Id = Guid.NewGuid() }
            });


        }

    }

}
