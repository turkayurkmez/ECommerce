using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.Catalog.Infrastructure.Persistence
{
    public static class CatalogDbInitializer
    {
        //MigrateAsync
        public static async Task MigrateAsync(IServiceProvider services, ILogger logger)
        {
            using (var scope = services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<CatalogDbContext>();
                try
                {
                    if (dbContext.Database.IsSqlServer())
                    {
                        await dbContext.Database.MigrateAsync();
                        logger.LogInformation("Database Migration işlemi tamamlandı");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Database Migration işlemi sırasında hata oluştu");
                    throw;
                }
            }
        }

    }
}
