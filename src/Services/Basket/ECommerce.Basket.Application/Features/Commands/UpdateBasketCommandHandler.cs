using ECommerce.Basket.Domain.Entities;
using ECommerce.Basket.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Features.Commands
{
    public record UpdateBasketCommand(string UserId, string UserName, List<BasketItemDto> Items) : IRequest<Result<ShoppingCart>>;
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, Result<ShoppingCart>>
    {

        private readonly IBasketRepository _basketRepository;
        public UpdateBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<Result<ShoppingCart>> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            //Sepet güncelleme işlemi:
            var shoppingCart = new ShoppingCart(request.UserId, request.UserName);
            foreach (var item in request.Items)
            {
                shoppingCart.AddItem(item.ProductId, item.ProductName, item.ProductImageUrl, item.Price, item.Quantity);
            }
            var updatedShoppingCart = await _basketRepository.UpdateBasketAsync(shoppingCart, cancellationToken);
            if (updatedShoppingCart == null)
            {
                return Result<ShoppingCart>.Failure("Sepet güncellenemedi");
            }
            //Sepeti veritabanına kaydetme işlemi:
            return Result<ShoppingCart>.Success(updatedShoppingCart);

        }
    }
}
