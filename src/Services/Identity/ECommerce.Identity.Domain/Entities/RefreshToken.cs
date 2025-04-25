using ECommerce.Common.Domain;

namespace ECommerce.Identity.Domain.Entities
{
    public class RefreshToken : Entity<int>, IAggregateRoot
    {
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpiryDate { get; private set; }
        public bool IsRevoked { get; private set; }
        //IP Address
        public string? IPAddress { get; private set; }
        //UserID
        public Guid UserId { get; private set; }

        //Navigation Properties
        public User User { get; private set; }

        protected RefreshToken()
        {
        }

        public RefreshToken(string token, DateTime expiryDate, string? ipAddress)
        {
            Token = token;
            ExpiryDate = expiryDate;
            IPAddress = ipAddress;
            IsRevoked = false;
        }

        public void Revoke()
        {
            IsRevoked = true;
            SetModifiedDate();
        }

        public bool IsActive => !IsRevoked && ExpiryDate > DateTime.UtcNow;


    }
}
