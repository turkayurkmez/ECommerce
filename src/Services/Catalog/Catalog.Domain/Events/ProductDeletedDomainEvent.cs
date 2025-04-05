using MediatR;

namespace ECommerce.Catalog.Domain.Events
{
    //product deleted domain event
    public record ProductDeletedDomainEvent(int ProductId) : INotification;

}
