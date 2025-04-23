using ECommerce.Common.Domain;

namespace ECommerce.Identity.Domain.Entities
{
    public class UserRole : Entity<int>
    {
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
        //Navigation Properties
        public User User { get; private set; }
        public Role Role { get; private set; }
        protected UserRole()
        {
        }
        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }

}
