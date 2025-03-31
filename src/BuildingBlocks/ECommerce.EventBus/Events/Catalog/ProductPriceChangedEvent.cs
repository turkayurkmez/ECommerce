namespace ECommerce.EventBus.Events.Catalog
{
    //product price changed event
    public record ProductPriceChangedEvent : IntegrationEvent
    {
        public int ProductId { get; init; }
        public decimal Price { get; init; }
        public decimal OldPrice { get; init; }
    }

}
