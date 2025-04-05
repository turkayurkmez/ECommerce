using ECommerce.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.EventHandlers;

public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
{
    
    private readonly ILogger<ProductCreatedDomainEventHandler> _logger;

    public ProductCreatedDomainEventHandler(ILogger<ProductCreatedDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    

    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {

        _logger.LogInformation($"Yeni ürün oluşturuldu: {notification.ProductId}, {notification.ProductName}, {notification.Price}, {notification.StockQuantity}");
        //Burada daha sonra masstransit gibi bir sistemle başka bir servise mesaj göndereceğiz.

    }
}





