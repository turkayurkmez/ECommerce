using Catalog.Domain.Entities;
using ECommerce.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);

        //sayfalama işlemi için:
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId, int skip, int take, CancellationToken cancellationToken = default);
        Task<List<Product>> GetProductsByBrandAsync(int brandId, CancellationToken cancellationToken = default);
        Task<List<Product>> GetProductsByBrandAsync(int brandId, int skip, int take, CancellationToken cancellationToken = default);




    }

    //Category Repository:

    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<List<Category>> GetRootCategoriesAsync(CancellationToken cancellationToken = default);
        Task<List<Category>> GetChildCategoriesAync(int parentId, CancellationToken cancellationToken = default);

    }

    //Brand Repository:

    public interface IBrandRepository : IRepository<Brand, int>
    {
        Task<List<Brand>> GetActiveBrandsAsync(CancellationToken cancellationToken = default);
    }



}
