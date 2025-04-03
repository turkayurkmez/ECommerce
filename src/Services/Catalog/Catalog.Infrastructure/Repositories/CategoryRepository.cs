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
    public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Category>> GetChildCategoriesAync(int parentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories
                .Include(c => c.SubCategories)
                .Include(c => c.ParentCategory)
                .Where(c => c.ParentCategoryId == parentId)
                .ToListAsync(cancellationToken);

        }

        public async Task<List<Category>> GetRootCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories                
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync(cancellationToken);

        }

       
    }
    
}
