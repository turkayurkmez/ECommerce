using ECommerce.Basket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Domain.Repositories
{
    public interface IBasketRepository
    {
        //Get basket by userId
        Task<ShoppingCart> GetBasketAsync(string userId, CancellationToken cancellationToken = default);
        //update basket
        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
        //delete basket
        Task DeleteBasketAsync(string userId, CancellationToken cancellationToken = default);



    }
}
