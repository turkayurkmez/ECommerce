using ECommerce.Common.Domain;
using ECommerce.Identity.Domain.Events;

namespace ECommerce.Identity.Domain.Entities
{
    public class Role : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        //Navigation Properties
        private readonly List<UserRole> _users = new List<UserRole>();
        public IReadOnlyCollection<UserRole> Users => _users.AsReadOnly();

        protected Role()
        {
        }
        public Role(string name, string description)
        {
            Name = name;
            Description = description;

            AddDomainEvent(new RoleCreatedDomainEvent(Id, name));
        }

        //update description
        public void UpdateDescription(string description)
        {
            Description = description;
            SetModifiedDate();

            AddDomainEvent(new RoleUpdatedDomainEvent(Id, Name));
        }


    }

}
