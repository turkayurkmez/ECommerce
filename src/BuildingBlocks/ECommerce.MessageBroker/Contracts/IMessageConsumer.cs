using ECommerce.EventBus.Contracts;

namespace ECommerce.MessageBroker.Contracts
{
    public interface IMessageConsumer<in T> where T : class, IIntegrationEvent
    {
        Task ConsumeAsync(T message, CancellationToken cancellationToken = default);
    }

}
