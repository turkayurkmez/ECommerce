namespace ECommerce.MessageBroker.Configuration
{
    public class MessageBrokerSettings
    {
        public string ServiceName { get; set; } = string.Empty;
        public RabbitMQSettings RabbitMQ { get; set; } = new();
    }

    public class RabbitMQSettings
    {
        //Host, VirtualHost, UserName, Password, Port
        public string Host { get; set; } = "localhost";
        public string VirtualHost { get; set; } = "/";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public int Port { get; set; } = 5672;
    }
}
