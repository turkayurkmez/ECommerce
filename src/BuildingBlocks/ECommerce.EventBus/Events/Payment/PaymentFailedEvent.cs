namespace ECommerce.EventBus.Events.Payment
{
    public record PaymentFailedEvent : IntegrationEvent
    {
        public Guid OrderId { get; init; }
        public string PaymentId { get; init; }
        public decimal Amount { get; init; }
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
