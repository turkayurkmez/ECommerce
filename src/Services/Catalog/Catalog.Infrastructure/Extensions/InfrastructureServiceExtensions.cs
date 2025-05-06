using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Catalog.Infrastructure.Persistence;
using ECommerce.Catalog.Infrastructure.Repositories;
using ECommerce.MessageBroker.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Catalog.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {


        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CatalogConnection"), sqlServerOptionsAction: sqlOption =>
            {
                sqlOption.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName);
                sqlOption.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

            }));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();

            services.AddMessageBroker(configuration);

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(InfrastructureServiceExtensions).Assembly);
            });

            return services;
        }
    }
}
