using ECommerce.Catalog.Infrastructure.Persistence;
using ECommerce.Common.Domain;
using ECommerce.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T, TId> : IRepository<T, TId>
                                                 where T : Entity<TId>, IAggregateRoot
                                                 where TId : IEquatable<TId>
    {

        protected readonly CatalogDbContext _dbContext;

        public RepositoryBase(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);


        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        public async Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().AnyAsync(e => e.Id.Equals(id), cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public Task<List<T>> GetAllAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<T>().Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public virtual async Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
