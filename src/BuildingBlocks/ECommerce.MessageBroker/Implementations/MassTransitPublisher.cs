using ECommerce.EventBus.Contracts;
using ECommerce.MessageBroker.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ECommerce.MessageBroker.Implementations
{
    public class MassTransitPublisher : IMessagePublisher
    {
        private readonly IBus _bus;
        private readonly ILogger<MassTransitPublisher> _logger;

        public MassTransitPublisher(IBus bus, ILogger<MassTransitPublisher> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class, IIntegrationEvent
        {
            try
            {
                _logger.LogInformation($"{message.Id} id'li {typeof(T).Name} tipindeki mesaj yayımlanıyor");
                await _bus.Publish(message, cancellationToken);

                //mesaj yayımlandıktan sonra loglama yap:
                _logger.LogInformation($"{message.Id} id'li {typeof(T).Name} tipindeki mesaj yayımlandı");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{message.Id} id'li {typeof(T).Name} tipindeki mesaj yayımlanırken bir hata oluştu");

                throw;
            }
        }


        public async Task PublishAsync<T>(T message, string routingKey, CancellationToken cancellationToken) where T : class, IIntegrationEvent
        {


            try
            {
                _logger.LogInformation($"{message.Id} id'li {typeof(T).Name} tipindeki mesaj yayımlanıyor");
                await _bus.Publish(message, context =>
                {
                    context.SetRoutingKey(routingKey);
                }, cancellationToken);


                _logger.LogInformation($"{message.Id} id'li {typeof(T).Name} tipindeki mesaj yayımlandı");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{message.Id} id'li {typeof(T).Name} tipindeki mesaj yayımlanırken bir hata oluştu");

                throw;
            }


        }
    }
}
