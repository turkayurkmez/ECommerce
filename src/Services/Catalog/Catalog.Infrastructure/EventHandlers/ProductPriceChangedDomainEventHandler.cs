using ECommerce.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.EventHandlers;

//Product price changed domain event handler
public class ProductPriceChangedDomainEventHandler : INotificationHandler<ProductPriceChangedDomainEvent>
{
    private readonly ILogger<ProductPriceChangedDomainEventHandler> _logger;

    public ProductPriceChangedDomainEventHandler(ILogger<ProductPriceChangedDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Ürün fiyatı değişti: {notification.ProductId}, {notification.OldPrice}, {notification.NewPrice}");
        //Burada daha sonra masstransit gibi bir sistemle başka bir servise mesaj göndereceğiz.
    }
}





