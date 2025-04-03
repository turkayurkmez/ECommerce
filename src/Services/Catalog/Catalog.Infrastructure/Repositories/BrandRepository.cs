using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Repositories
{

    //Get Active Brands içeren BrandRepository sınıfı oluştur:
    public class BrandRepository : RepositoryBase<Brand, int>, IBrandRepository
    {
        public BrandRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Brand>> GetActiveBrandsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Brands
                .Where(b => b.IsActive)
                .ToListAsync(cancellationToken);
        }
    }
   
}
