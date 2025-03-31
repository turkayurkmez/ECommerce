namespace ECommerce.EventBus.Events.Order
{
    public record OrderStatusChangedEvent : IntegrationEvent
    {
        public Guid OrderId { get; init; }
        public string OldStatus { get; init; } = string.Empty;
        public string NewStatus { get; init; } = string.Empty;

    }
}
