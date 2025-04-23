using ECommerce.Common.Repositories;
using ECommerce.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        //Get with roles async
        Task<User?> GetWithRolesAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsUserNameInUniqueAsync(string userName, CancellationToken cancellationToken = default);
    }

    public interface IRoleRepository : IRepository<Role, Guid>
    {
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);

        //Get default roles async
        Task<List<Role>> GetDefaultRolesAsync(CancellationToken cancellationToken = default);
    }

    //refresh token repository
    public interface IRefreshTokenRepository : IRepository<RefreshToken, int>
    {
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
       

    }
}
