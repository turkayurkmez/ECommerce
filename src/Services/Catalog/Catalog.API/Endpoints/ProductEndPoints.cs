using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Application.Features.Products.Commands;
using ECommerce.Catalog.Application.Features.Products.Queries;
using ECommerce.Common.Models;
using MediatR;

namespace ECommerce.Catalog.API.Endpoints
{
    public static class ProductEndPoints
    {
        public static WebApplication MapProductsEndPoints(this WebApplication app)
        {
            //"products" endpointi için bir grup oluştur
            var group = app.MapGroup("/products").WithTags("Products")
                                                 .WithOpenApi();

            //GetAllProducts with Pagination:
            group.MapGet("/", async (int  pageNumber=1, int pageSize=10, IMediator mediator = null!, CancellationToken cancellationToken=default) =>
            {
                var pagination = new PaginationParameters() { PageNumber = pageNumber, PageSize = pageSize };
                var query = new GetAllProductsQuery(pagination);
                var result = await mediator.Send(query, cancellationToken);
                return result.IsSuccess ? Results.Ok(result):Results.BadRequest(result.Errors);
            }).WithName("GetAllProducts")
              .Produces<ApiResponse<List<ProductSummaryDto>>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Tüm Ürünleri Sayfalama ile getirir");


            //GetProductById:
            group.MapGet("/{id:int}", async (int id, IMediator mediator = null!, CancellationToken cancellationToken = default) =>
            {
                var query = new GetProductByIdQuery(id);
                var result = await mediator.Send(query, cancellationToken);
                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            }).WithName("GetProductById")
              .Produces<ApiResponse<ProductDto>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Id ile Ürünü getirir");

            //Get Products by CategoryId:

            group.MapGet("/category/{categoryId:int}", async (int categoryId, int pageNumber=1, int pageSize=10, IMediator mediator = null!, CancellationToken cancellationToken=default) =>
            {
                var pagination = new PaginationParameters() { PageNumber = pageNumber, PageSize = pageSize };
                var query = new GetProductsByCategoryQuery(categoryId, pagination);
                var result = await mediator.Send(query, cancellationToken);
                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            }).WithName("GetProductsByCategoryId")
              .Produces<ApiResponse<List<ProductDto>>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Kategori Id ile Ürünleri getirir");

            //Get Products by BrandId with pagination:

            group.MapGet("/brand/{brandId:int}", async (int brandId, int pageNumber = 1, int pageSize = 10, IMediator mediator = null!, CancellationToken cancellationToken = default) =>
            {
                var pagination = new PaginationParameters() { PageNumber = pageNumber, PageSize = pageSize };
                var query = new GetProductsByBrandQuery(brandId, pagination);
                var result = await mediator.Send(query, cancellationToken);
                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            }).WithName("GetProductsByBrandId")
              .Produces<ApiResponse<List<ProductDto>>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Marka Id ile Ürünleri getirir");

            //CreateProduct:

            group.MapPost("/", async (CreateProductDto productDto, IMediator mediator = null!, CancellationToken cancellationToken = default) =>
            {
                var command = new CreateProductCommand(productDto);
                var result = await mediator.Send(command, cancellationToken);
                return result.IsSuccess ? Results.Created($"/products/{result.Data}",result) : Results.BadRequest(result);
            }).WithName("CreateProduct")
              .Produces<ApiResponse<ProductDto>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Yeni Ürün oluşturur");

            //UpdateProduct:

            group.MapPut("/{id:int}", async (int id, UpdateProductDto productDto, IMediator mediator = null!, CancellationToken cancellationToken = default) =>
            {
                if (id != productDto.Id)
                {
                    return Results.BadRequest("Id'ler uyuşmuyor");
                }
                var command = new UpdateProductCommand(productDto);
                var result = await mediator.Send(command, cancellationToken);
                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            }).WithName("UpdateProduct")
              .Produces<ApiResponse<ProductDto>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Ürünü günceller");

            //DeleteProduct:
            group.MapDelete("/{id:int}", async (int id, IMediator mediator = null!, CancellationToken cancellationToken = default) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await mediator.Send(command, cancellationToken);
                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            }).WithName("DeleteProduct")
              .Produces<ApiResponse<ProductDto>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .WithDescription("Ürünü siler");


            return app;



        }
    }
}
