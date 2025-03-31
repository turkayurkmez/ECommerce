namespace ECommerce.EventBus.Events.Payment
{
    public record PaymentCompletedEvent : IntegrationEvent
    {
        public Guid OrderId { get; init; }
        public string PaymentId { get; init; }

        public decimal Amount { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
