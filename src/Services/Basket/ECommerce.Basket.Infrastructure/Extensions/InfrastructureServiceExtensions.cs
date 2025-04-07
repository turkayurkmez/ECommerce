using ECommerce.Basket.Domain.Repositories;
using ECommerce.Basket.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ECommerce.Basket.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Redis bağlantısı için gerekli olan kodlar burada olacak.
            //Örnek olarak StackExchange.Redis kütüphanesini kullanabilirsiniz.
            services.AddSingleton
                  <IConnectionMultiplexer>(sp =>
              {
                  var configurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"), true);
                  configurationOptions.AbortOnConnectFail = false;

                  return ConnectionMultiplexer.Connect(configurationOptions);

              });

            services.AddSingleton<IBasketRepository, RedisBasketRepository>();
            return services;
        }
    }
}
