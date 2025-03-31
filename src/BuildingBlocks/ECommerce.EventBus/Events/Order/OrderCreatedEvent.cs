namespace ECommerce.EventBus.Events.Order
{
    public record OrderCreatedEvent : IntegrationEvent
    {
        public Guid OrderId { get; init; }
        public string BuyerId { get; init; }
        public string BuyerName { get; init; }
        public string BuyerEmail { get; init; }
        public decimal Total { get; init; }
        public string Status { get; init; } = string.Empty;
        public List<OrderItem> OrderItems { get; init; }
    }

    public record OrderItem
    {
        public Guid ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal UnitPrice { get; init; }
        public int Units { get; init; }
    }
}
