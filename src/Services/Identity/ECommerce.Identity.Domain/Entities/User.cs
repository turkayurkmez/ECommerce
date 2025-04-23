using ECommerce.Common.Domain;
using ECommerce.Identity.Domain.Events;

namespace ECommerce.Identity.Domain.Entities
{
    public class User : AuditableEntity<Guid>, IAggregateRoot
    {
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;

        //IsActive
        public bool IsActive { get; private set; }
        //EmailConfirmed
        public bool EmailConfirmed { get; private set; }
        public DateTime? LastLoginDate { get; set; }

        private readonly List<UserRole> _roles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        protected User()
        {
        }
        public User(string userName, string email, string firstName, string lastName, string passwordHash)
        {
            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
            IsActive = true;
            EmailConfirmed = false;

            AddDomainEvent(new UserCreatedDomainEvent(Id, userName, email));

        }

        //change email
        public void ChangeEmail(string email)
        {
            Email = email;
            EmailConfirmed = false;
            SetModifiedDate();
            AddDomainEvent(new UserUpdatedDomainEvent(Id, UserName, email));
        }

        //email confirmed
        public void ConfirmEmail()
        {
            EmailConfirmed = true;
            SetModifiedDate();

            AddDomainEvent(new UserEmailConfirmedDomainEvent(Id, UserName,Email));
        }

        //change password
        public void ChangePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            SetModifiedDate();

            AddDomainEvent(new UserPasswordChangedDomainEvent(Id, UserName));
        }

        //update profile
        public void UpdateProfile(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            SetModifiedDate();

            AddDomainEvent(new UserUpdatedDomainEvent(Id,UserName,Email));
        }

        //Deactivate:
        public void Deactivate()
        {
            IsActive = false;
            SetModifiedDate();

            AddDomainEvent(new UserUpdatedDomainEvent(Id, UserName, Email));
        }

        //Activate:
        public void Activate()
        {
            IsActive = true;
            SetModifiedDate();
            AddDomainEvent(new UserUpdatedDomainEvent(Id, UserName, Email));
        }

        //Add Role
        public void AddRole(Role role)
        {
            if (!_roles.Any(r => r.RoleId == role.Id))
            {
                _roles.Add(new UserRole(Id, role.Id));
                SetModifiedDate();
                AddDomainEvent(new UserRoleAddedDomainEvent(Id,role.Name));
            }
        }

        //Remove Role
        public void RemoveRole(Role role)
        {
            var userRole = _roles.FirstOrDefault(r => r.RoleId == role.Id);
            if (userRole != null)
            {
                _roles.Remove(userRole);
                SetModifiedDate();
                AddDomainEvent(new UserRoleRemovedDomainEvent(Id, role.Name));
            }
        }

        //Add Refresh Token
        public void AddRefreshToken(string token, DateTime expiryDate, string ipAddress)
        {
            var refreshToken = new RefreshToken(token, expiryDate, ipAddress);
            _refreshTokens.Add(refreshToken);

            while (_refreshTokens.Count > 5)
            {
                var oldestToken = _refreshTokens.OrderBy(t => t.ExpiryDate).FirstOrDefault();
                if (oldestToken != null)
                {
                    _refreshTokens.Remove(oldestToken);
                }
            }

            SetModifiedDate();

        }

        //Revoke Refresh Token
        public void RevokeRefreshToken(string token)
        {
            var refreshToken = _refreshTokens.FirstOrDefault(t => t.Token == token);
            if (refreshToken != null)
            {
                refreshToken.Revoke();
                SetModifiedDate();
            }
        }

        //update last login date
        public void UpdateLastLoginDate()
        {
            LastLoginDate = DateTime.UtcNow;
            SetModifiedDate();
        }




    }
}
