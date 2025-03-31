namespace ECommerce.EventBus.Events.Catalog
{
    //Product deleted event
    public record ProductDeletedEvent : IntegrationEvent
    {
        public int ProductId { get; init; }
    }
}
