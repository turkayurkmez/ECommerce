using ECommerce.EventBus.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.EventBus.Extensions
{
    public static class EventBusExtensions
    {
        //Add Event Bus Extensions

        public static IServiceCollection AddEventBus(this IServiceCollection services,
                                                         IConfiguration configuration, string sectionName = "EventBus",
                                                         Action<IBusRegistrationConfigurator>? configureConsumers = null)
        {
            //bind settings from configuration
            var eventBusSettings = new EventBusSettings();
            configuration.GetSection(sectionName).Bind(eventBusSettings);

            services.AddSingleton(eventBusSettings);

            services.AddMassTransit(busConfig =>
            {
                configureConsumers?.Invoke(busConfig);
                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqSettings = eventBusSettings.RabbitMq;

                    cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.VirtualHost, h =>
                    {
                        h.Username(rabbitMqSettings.UserName);
                        h.Password(rabbitMqSettings.Password);
                    });

                    cfg.PrefetchCount = rabbitMqSettings.PrefetchCount;
                    cfg.ConcurrentMessageLimit = rabbitMqSettings.ConcurrentMessageLimit;

                    cfg.UseMessageRetry(retryConfig =>
                    {
                        retryConfig.Interval(rabbitMqSettings.RetryCount, rabbitMqSettings.RetryIntervalSeconds);
                    });

                    cfg.ConfigureEndpoints(context);

                });
            });

            return services;

        }

        public static IServiceCollection AddEventBusConsumers(this IServiceCollection services, Assembly assembly)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumers(assembly);
            });
            return services;
        }
    }


}
