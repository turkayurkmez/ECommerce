using ECommerce.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Repositories
{
    public interface IRepository<TEntity,TId> where TEntity : Entity<TId>, IAggregateRoot
                                              where TId : IEquatable<TId>
    {
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken =default);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        //Sayfalama işlemi için:
        Task<List<TEntity>> GetAllAsync(int skip, int take, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
