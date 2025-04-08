using ECommerce.Basket.API.Protos;
using ECommerce.Basket.Application.Features.Commands;
using ECommerce.Basket.Application.Features.Queries;
using ECommerce.Basket.Domain.Entities;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using System.Diagnostics;

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

        public async override Task<ShoppingCartResponse> UpdateBasket(UpdateBasketRequest request, ServerCallContext context)
        {
            var command = new UpdateBasketCommand(request.UserId, request.UserName, request.Items.Select(i=>new BasketItemDto(i.ProductId,i.ProductName,i.ImageUrl,i.Price,i.Quantity)).ToList());

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Sepet güncellenemedi"));
            }


            return MapToShoppingCartResponse(result.Data);

        }

        public async override Task<CheckoutResponse> Checkout(CheckoutRequest request, ServerCallContext context)
        {

            var command = new CheckoutBasketCommand()
            {
                UserId = request.UserId,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
                BillingAddress = request.BillingAddress,
                ShippingAddress = request.ShippingAddress,
                PaymentMethod = request.PaymentMethod
            };
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Sepet güncellenemedi"));
            }
            return new CheckoutResponse
            {
               Success = true,
                Message = result.Message
            };


        }

        //Delete Basket async:

        public async override Task<Empty> DeleteBasket(DeleteBasketRequest request, ServerCallContext context)
        {
            var command = new DeleteBasketCommand(request.UserId);
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Sepet silinemedi"));
            }
            return new Empty();
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
