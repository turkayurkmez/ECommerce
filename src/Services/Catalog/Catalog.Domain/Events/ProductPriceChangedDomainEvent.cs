using MediatR;

namespace ECommerce.Catalog.Domain.Events
{
    //product price changed domain event
    public record ProductPriceChangedDomainEvent(int ProductId, decimal OldPrice, decimal NewPrice) : INotification;

}
