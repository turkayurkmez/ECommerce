using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
    {
        public ProductRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Product>> GetProductsByBrandAsync(int brandId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.BrandId == brandId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> GetProductsByBrandAsync(int brandId, int skip, int take, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                 .Include(p => p.ProductImages)
                 .Include(p => p.Category)
                 .Include(p => p.Brand)
                 .Where(p => p.BrandId == brandId)
                 .Skip(skip)
                 .Take(take)
                 .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                 .Include(p => p.ProductImages)
                 .Include(p => p.Category)
                 .Include(p => p.Brand)
                 .Where(p => p.CategoryId == categoryId)
                 .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId, int skip, int take, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.CategoryId == categoryId)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Products
                   .Include(p => p.ProductImages)
                   .Include(p => p.Category)
                   .Include(p => p.Brand)
                   .Where(p => p.CategoryId == categoryId)
                   .ToListAsync();
        }
    }

}
