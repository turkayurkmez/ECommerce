using ECommerce.MessageBroker.Configuration;
using ECommerce.MessageBroker.Implementations;
using ECommerce.MessageBroker.Interfaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.MessageBroker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, string sectionName = "MessageBroker", Action<IBusRegistrationConfigurator>? configureConsumers = null)
        {
            var messageBrokerSettings = new MessageBrokerSettings();
            configuration.GetSection(sectionName).Bind(messageBrokerSettings);

            services.AddSingleton(messageBrokerSettings);

            services.AddScoped<IMessagePublisher, MassTransitPublisher>();

            services.AddMassTransit(busConfig =>
            {
                configureConsumers?.Invoke(busConfig);
                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqSettings = messageBrokerSettings.RabbitMQ;
                    cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.VirtualHost, h =>
                    {
                        h.Username(rabbitMqSettings.UserName);
                        h.Password(rabbitMqSettings.Password);
                    });

                    cfg.UseMessageRetry(retryConfig =>
                    {
                        retryConfig.Interval(3, TimeSpan.FromSeconds(5));
                    });
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(messageBrokerSettings.ServiceName, false));
                });
            });
            return services;

        }

        //Add Message Consumers:
        public static IServiceCollection AddMessageConsumer<T>(this IServiceCollection services, Assembly assembly) where T : class, IConsumer
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumers(assembly);
            });

            return services;
        }
    }
}
