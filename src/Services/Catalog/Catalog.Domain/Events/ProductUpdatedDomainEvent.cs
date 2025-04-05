using MediatR;

namespace ECommerce.Catalog.Domain.Events
{
    //product updated domain event
    public record ProductUpdatedDomainEvent(int ProductId, string ProductName) : INotification;

}
