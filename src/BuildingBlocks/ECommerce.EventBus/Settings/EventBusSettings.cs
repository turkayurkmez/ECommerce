namespace ECommerce.EventBus.Settings
{
    public record EventBusSettings
    {
        public string ServiceName { get; set; } = string.Empty;
        public RabbitMqSettings RabbitMq { get; set; } = new();
    }

}
