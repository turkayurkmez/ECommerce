using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Catalog.Infrastructure.Persistence;
using ECommerce.Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Extensions
{
    public static  class InfrastructureServiceExtensions
    {


        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // add your infrastructure services here
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CatalogConnection"), sqlServerOptionsAction: sqlOption =>
            {
                sqlOption.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName);
                sqlOption.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

            }));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();

            return services;
        }
    }
}
