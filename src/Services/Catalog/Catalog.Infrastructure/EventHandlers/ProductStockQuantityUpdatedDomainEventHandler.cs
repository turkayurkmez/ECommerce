using ECommerce.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.EventHandlers;

//Product stock quantity updated domain event handler
public class ProductStockQuantityUpdatedDomainEventHandler : INotificationHandler<ProductStockQuantityUpdatedDomainEvent>
{
    private readonly ILogger<ProductStockQuantityUpdatedDomainEventHandler> _logger;

    public ProductStockQuantityUpdatedDomainEventHandler(ILogger<ProductStockQuantityUpdatedDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(ProductStockQuantityUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Ürün stok miktarı güncellendi: {notification.ProductId}, {notification.OldStockQuantity}, {notification.NewStockQuantity}");
        //Burada daha sonra masstransit gibi bir sistemle başka bir servise mesaj göndereceğiz.
    }
}





