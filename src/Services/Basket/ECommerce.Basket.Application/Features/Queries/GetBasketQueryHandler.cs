using ECommerce.Basket.Domain.Entities;
using ECommerce.Basket.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Features.Queries
{
    public record GetBasketQuery(string UserId) : IRequest<Result<ShoppingCart>>;
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, Result<ShoppingCart>>
    {
        private readonly IBasketRepository _basketRepository;
        public GetBasketQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<Result<ShoppingCart>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {

            //Sepeti veritabanından alma işlemi:
            var shoppingCart = await _basketRepository.GetBasketAsync(request.UserId, cancellationToken);
            if (shoppingCart == null)
            {
                return Result<ShoppingCart>.Success(new ShoppingCart(request.UserId, request.UserId));
            }
            //Sepeti döndürme işlemi:
            return Result<ShoppingCart>.Success(shoppingCart);

        }
    }
}
