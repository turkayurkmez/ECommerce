using ECommerce.EventBus.Contracts;

namespace ECommerce.EventBus.Events
{
    public abstract record IntegrationEvent : IIntegrationEvent
    {
        public Guid Id => Guid.NewGuid();

        public DateTime CreationDate => DateTime.UtcNow;
    }

}
