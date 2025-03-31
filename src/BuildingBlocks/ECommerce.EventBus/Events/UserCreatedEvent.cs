namespace ECommerce.EventBus.Events
{
    public record UserCreatedEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;


    }


}
