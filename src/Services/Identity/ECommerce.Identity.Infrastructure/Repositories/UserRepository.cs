using ECommerce.Identity.Domain.Entities;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Identity.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        private readonly IdentityDbContext _dbContext;
        public UserRepository(IdentityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                 .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                 .FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }

        public async Task<User?> GetWithRolesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                 .Include(u => u.Roles)
                 .ThenInclude(ur => ur.Role)
                 .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Users
                 .AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> IsUserNameInUniqueAsync(string userName, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Users
                 .AnyAsync(u => u.UserName == userName, cancellationToken);
        }
        // Implement any additional methods specific to user repository here
    }

}
