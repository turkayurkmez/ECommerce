namespace ECommerce.EventBus.Events
{
    //User password changed event
    public record UserPasswordChangedEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
    }


}
