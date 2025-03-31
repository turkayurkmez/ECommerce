namespace ECommerce.EventBus.Events
{
    //User deleted event
    public record UserDeletedEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;

    }


}
