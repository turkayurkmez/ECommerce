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
    public record BasketItemDto(int ProductId, string ProductName, string ProductImageUrl, decimal Price, int Quantity);
    public record CreateBasketCommand(string UserId, string UserName, List<BasketItemDto> Items) : IRequest<Result<ShoppingCart>>;
    internal class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, Result<ShoppingCart>>
    {
     
        private readonly IBasketRepository _basketRepository; 
        public CreateBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<Result<ShoppingCart>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            //Sepet oluşturma işlemi:
            var shoppingCart = new ShoppingCart(request.UserId, request.UserName);
            foreach (var item in request.Items)
            {
                shoppingCart.AddItem(item.ProductId, item.ProductName, item.ProductImageUrl, item.Price, item.Quantity);
            }

            var updatedShoppingCart = await _basketRepository.UpdateBasketAsync(shoppingCart, cancellationToken);
            if (updatedShoppingCart == null)
            {
                return Result<ShoppingCart>.Failure("Sepet oluşturulamadı");
            }

           //Sepeti veritabanına kaydetme işlemi:

          
            return Result<ShoppingCart>.Success(updatedShoppingCart);

        }
    }
}
