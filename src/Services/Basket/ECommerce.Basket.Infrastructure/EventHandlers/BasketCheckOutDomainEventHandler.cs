using ECommerce.Basket.Domain.Events;
using ECommerce.EventBus.Events.Basket;
using ECommerce.MessageBroker.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using BasketCheckoutItemEvent = ECommerce.EventBus.Events.Basket.BasketCheckoutItemEvent;

namespace ECommerce.Basket.Infrastructure.EventHandlers
{
    public class BasketCheckOutDomainEventHandler : INotificationHandler<BasketCheckOutDomainEvent>
    {

        private readonly ILogger<BasketCheckOutDomainEventHandler> _logger;
        private readonly IMessagePublisher _messagePublisher;
        public BasketCheckOutDomainEventHandler(IMessagePublisher messagePublisher, ILogger<BasketCheckOutDomainEventHandler> logger)
        {

            _logger = logger;
            _messagePublisher = messagePublisher;
        }
        public async Task Handle(BasketCheckOutDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new BasketCheckoutEvent
            {
                UserId = notification.UserId,
                UserName = notification.UserName,
                BillingAddress = notification.BillingAddress,
                ShippingAddress = notification.ShippingAddress,
                PaymentMethod = notification.PaymentMethod,
                Items = notification.Items.Select(i => new BasketCheckoutItemEvent
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity,


                }).ToList()
            };

            await _messagePublisher.PublishAsync(integrationEvent, cancellationToken);
            _logger.LogInformation("BasketCheckOutDomainEventHandler: Sepet onaylandı olayı rabbitMQ'ya iletildi.");

        }
    }

}
