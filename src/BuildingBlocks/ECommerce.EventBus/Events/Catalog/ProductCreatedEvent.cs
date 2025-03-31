namespace ECommerce.EventBus.Events.Catalog
{
    public record ProductCreatedEvent : IntegrationEvent
    {
        public int ProductId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public int CategoryId { get; init; }
        public string CategoryName { get; init; } = string.Empty;
    }



}
