using ECommerce.Common.Domain;
using ECommerce.Identity.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Identity.Infrastructure.Persistence
{
    public class IdentityDbContext : DbContext
    {
        private readonly IMediator? _mediator;
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IMediator? mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //değişikikleri kaydetmeden önce domain eventleri topla:
            var domainEntities = ChangeTracker.Entries<Entity<Guid>>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.ClearDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            //domain eventleri publish et:
            if (_mediator != null && domainEvents.Any())
            {
                foreach (var domainEvent in domainEvents)
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }
            }
            return result;
        }
    }
}
