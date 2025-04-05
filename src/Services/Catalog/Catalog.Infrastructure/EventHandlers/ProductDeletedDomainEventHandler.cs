using ECommerce.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.EventHandlers;

//Product deleted domain event handler
public class ProductDeletedDomainEventHandler : INotificationHandler<ProductDeletedDomainEvent>
{
    private readonly ILogger<ProductDeletedDomainEventHandler> _logger;

    public ProductDeletedDomainEventHandler(ILogger<ProductDeletedDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Ürün silindi: {notification.ProductId}");
        //Burada daha sonra masstransit gibi bir sistemle başka bir servise mesaj göndereceğiz.
    }
}





