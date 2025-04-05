using ECommerce.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.EventHandlers;

//Product updated domain event handler
public class ProductUpdatedDomainEventHandler : INotificationHandler<ProductUpdatedDomainEvent>
{
    private readonly ILogger<ProductUpdatedDomainEventHandler> _logger;

    public ProductUpdatedDomainEventHandler(ILogger<ProductUpdatedDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Ürün güncellendi: {notification.ProductId}, {notification.ProductName}");
        //Burada daha sonra masstransit gibi bir sistemle başka bir servise mesaj göndereceğiz.
    }
}





