
using ECommerce.Basket.Application.Features.Commands;
using ECommerce.Basket.Application.Features.Queries;
using ECommerce.Basket.Domain.Entities;
using ECommerce.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ECommerce.Basket.API.Endpoints
{
    public static class BasketEndpoints
    {
        public static void MapBasketEndpoints(this WebApplication app)
        {
            //create mapGroup with / basket url
            var group = app.MapGroup("/basket").WithTags("Basket").WithOpenApi();

            //get basket by userId with IMediator
            group.MapGet("/{userId}", async (string userId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new GetBasketQuery(userId);
                var result = await mediator.Send(query, cancellationToken);
                return result.IsSuccess 
                         ? Results.Ok(result) 
                         : Results.BadRequest(result.Errors);
            }).WithName("GetBasket")
              .Produces<ApiResponse<ShoppingCart>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Sepeti getirir");

            //update basket:
            app.MapPost("/", async (UpdateBasketCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);
                return result.IsSuccess
                         ? Results.Ok(ApiResponse<ShoppingCart>.FromResult(result))
                         : Results.BadRequest(ApiResponse<ShoppingCart>.Failure(result.Message));
            }).WithName("UpdateBasket")
              .Produces<ApiResponse<ShoppingCart>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Sepeti günceller");


            //checkout basket:
            app.MapPost("/checkout", async (CheckoutBasketCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.IsSuccess
                         ? Results.Ok(ApiResponse<string>.Success(result.Message))
                         : Results.BadRequest(ApiResponse<string>.Failure(result.Message));
            }).WithName("CheckoutBasket")
              .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Sepeti onaylar");


            //delete basket:
            app.MapDelete("/{userId}", async (string userId, IMediator mediator) =>
            {
                var command = new DeleteBasketCommand(userId);
                var result = await mediator.Send(command);
                return result.IsSuccess
                         ? Results.Ok(ApiResponse<string>.Success(result.Message))
                         : Results.BadRequest(ApiResponse<string>.Failure(result.Message));
            }).WithName("DeleteBasket")
              .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Sepeti siler");

        }
    }
}
