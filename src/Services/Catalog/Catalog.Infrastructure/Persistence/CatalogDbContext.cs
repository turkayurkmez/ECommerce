using ECommerce.Catalog.Domain.Entities;
using ECommerce.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        private readonly IMediator? _mediator;

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IMediator? mediator) : base(options)
        {
            _mediator = mediator;
        }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        //Products:
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker
                .Entries<Entity<int>>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);
            if (_mediator != null)
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
