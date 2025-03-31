using ECommerce.EventBus.Contracts;

namespace ECommerce.MessageBroker.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class, IIntegrationEvent;

        Task PublishAsync<T>(T message, string routingKey, CancellationToken cancellationToken = default) where T : class, IIntegrationEvent;


    }


}
