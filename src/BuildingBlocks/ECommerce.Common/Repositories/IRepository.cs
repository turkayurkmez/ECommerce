using ECommerce.Common.Domain;

namespace ECommerce.Common.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : Entity<TId>, IAggregateRoot
                                              where TId : IEquatable<TId>
    {
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        //Sayfalama işlemi için:
        Task<List<TEntity>> GetAllAsync(int skip, int take, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
