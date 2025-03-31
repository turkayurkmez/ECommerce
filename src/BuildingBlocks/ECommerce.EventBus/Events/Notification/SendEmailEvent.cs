namespace ECommerce.EventBus.Events.Notification
{
    public record SendEmailEvent : IntegrationEvent
    {
        public string To { get; init; } = string.Empty;
        public string Subject { get; init; } = string.Empty;
        public string Body { get; init; } = string.Empty;
        public bool IsHtml { get; init; } = true;
    }
}
