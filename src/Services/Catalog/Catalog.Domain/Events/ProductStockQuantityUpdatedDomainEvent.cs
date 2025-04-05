using MediatR;

namespace ECommerce.Catalog.Domain.Events
{
    //product stock quantity updated domain event
    public record ProductStockQuantityUpdatedDomainEvent(int ProductId, int OldStockQuantity, int NewStockQuantity) : INotification;

}
