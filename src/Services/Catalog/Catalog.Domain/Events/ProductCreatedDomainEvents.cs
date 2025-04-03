using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Domain.Events
{
    public record ProductCreatedDomainEvent(int ProductId, string ProductName, decimal Price, int StockQuantity) : INotification;

    //product updated domain event
    public record ProductUpdatedDomainEvent(int ProductId, string ProductName) : INotification;

    //product price changed domain event
    public record ProductPriceChangedDomainEvent(int ProductId, decimal OldPrice, decimal NewPrice) : INotification;

    //product stock quantity updated domain event
    public record ProductStockQuantityUpdatedDomainEvent(int ProductId, int OldStockQuantity, int NewStockQuantity) : INotification;

    //product deleted domain event
    public record ProductDeletedDomainEvent(int ProductId) : INotification;

}
