namespace ECommerce.EventBus.Settings
{
    public record RabbitMqSettings
    {
        public string Host { get; init; } = "localhost";
        public string VirtualHost { get; set; } = "/";
        public int Port { get; set; } = 5672;
        public string UserName { get; init; } = "guest";
        public string Password { get; init; } = "guest";
        public ushort PrefetchCount { get; init; } = 16;

        public int RetryCount { get; set; } = 2;
        public int RetryIntervalSeconds { get; set; } = 2;
        public int ConcurrentMessageLimit { get; set; } = 10;


        public string ExchangeName { get; init; }
        public string RoutingKey { get; init; }


    }
}
