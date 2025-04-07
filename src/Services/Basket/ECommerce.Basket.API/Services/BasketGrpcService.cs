using ECommerce.Basket.API.Protos;
using ECommerce.Basket.Application.Features.Queries;
using ECommerce.Basket.Domain.Entities;
using Grpc.Core;
using MediatR;

namespace ECommerce.Basket.API.Services
{
    public class BasketGrpcService : BasketProtoService.BasketProtoServiceBase
    {
        private readonly IMediator _mediator;
        public BasketGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async override Task<ShoppingCartResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
        {
            var query = new GetBasketQuery(request.UserId);
            var result =  await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                throw new RpcException(new Status(StatusCode.NotFound,"Sepet bulunamadı"));
            }

            return MapToShoppingCartResponse(result.Data);



        }

        private ShoppingCartResponse MapToShoppingCartResponse(ShoppingCart? data)
        {
           var response = new ShoppingCartResponse
           {
               UserId = data.UserId,
               UserName = data.UserName
           };

            foreach (var item in data.Items)
            {
                response.Items.Add(new BasketItemResponse
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ImageUrl = item.ProductImageUrl,
                    Price = (double)item.Price,
                    Quantity = item.Quantity
                });

            }

            return response;

        }
    }
}
