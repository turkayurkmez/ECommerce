namespace ECommerce.EventBus.Events
{
    //User updated event
    public record UserUpdatedEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
    }


}
