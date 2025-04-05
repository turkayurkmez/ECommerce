using MediatR;

namespace ECommerce.Catalog.Domain.Events
{
    public record ProductCreatedDomainEvent(int ProductId, string ProductName, decimal Price, int StockQuantity) : INotification;

}
