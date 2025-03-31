using ECommerce.EventBus.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ECommerce.MessageBroker.Implementations
{
    public abstract class BaseConsumer<T> : IConsumer<T> where T : class, IIntegrationEvent
    {
        private readonly ILogger<BaseConsumer<T>> _logger;

        public BaseConsumer(ILogger<BaseConsumer<T>> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<T> context)
        {
            var message = context.Message;
            var messageType = typeof(T).Name;
            var messageID = message.Id;

            _logger.LogInformation($"{messageID} id'li {messageType} tipindeki mesaj tüketiliyor");

            try
            {
                await ProcessMessageAsync(message, context.CancellationToken);
                _logger.LogInformation($"{messageID} id'li {messageType} tipindeki mesaj tüketildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{messageID} id'li {messageType} tipindeki mesaj tüketilirken bir hata oluştu. Hata mesajı: {ex.Message}");


                throw;
            }

        }

        protected abstract Task ProcessMessageAsync(T message, CancellationToken cancellationToken);

    }
}
