using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Persistence
{
    public static  class CatalogDbInitializer 
    {
        //MigrateAsync
        public static async Task MigrateAsync(IServiceProvider services, ILogger logger)
        {
            try
            {
                var dbContext = services.GetRequiredService<CatalogDbContext>();
                //if is sql server
                if (dbContext.Database.IsSqlServer())
                {
                    await dbContext.Database.MigrateAsync();
                    logger.LogInformation("Database Migration işlemi tamamlandı");
                }
              

            }
            catch (Exception)
            {

                logger.LogError("Database Migration işlemi sırasında hata oluştu");
                throw;
            }
        }
       
    }
}
