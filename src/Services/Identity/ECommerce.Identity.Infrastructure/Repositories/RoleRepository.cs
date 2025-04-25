using ECommerce.Identity.Domain.Entities;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Identity.Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<Role, Guid>, IRoleRepository
    {
        private readonly IdentityDbContext _dbContext;
        public RoleRepository(IdentityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Roles.AnyAsync(x => x.Name == name, cancellationToken);
        }
        public async Task<List<Role>> GetDefaultRolesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Roles
                .Where(x => x.Name == "User")
                .ToListAsync(cancellationToken);

        }


    }

    public class RefreshTokenRepository : RepositoryBase<RefreshToken, int>, IRefreshTokenRepository
    {
        private readonly IdentityDbContext _dbContext;
        public RefreshTokenRepository(IdentityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
        }
        public async Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens
                .Where(x => x.UserId == userId && !x.IsRevoked && x.ExpiryDate > DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }
    }
}
