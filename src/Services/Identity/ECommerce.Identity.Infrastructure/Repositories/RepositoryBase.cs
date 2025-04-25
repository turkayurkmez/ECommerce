using ECommerce.Common.Domain;
using ECommerce.Common.Repositories;
using ECommerce.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Identity.Infrastructure.Repositories
{
    public class RepositoryBase<T, TId>(IdentityDbContext dbContext) : IRepository<T, TId>
                                         where T : Entity<TId>, IAggregateRoot

                                         where TId : IEquatable<TId>
    {
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().AnyAsync(e => e.Id.Equals(id), cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Entry(entity).State = EntityState.Modified;

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
