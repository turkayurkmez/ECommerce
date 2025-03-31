namespace ECommerce.EventBus.Events.Notification
{
    public record SendSmsEvent : IntegrationEvent
    {
        public string PhoneNumber { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
    }
}
