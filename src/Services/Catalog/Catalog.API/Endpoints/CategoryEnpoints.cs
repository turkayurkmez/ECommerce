using Catalog.Application.DTOs;
using Catalog.Application.Features.Categories.Commands;
using Catalog.Application.Features.Categories.Queries;
using ECommerce.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/categories")
                .WithTags("Categories");

            // Get all categories
            group.MapGet("/", async (IMediator mediator) =>
            {
                var query = new GetAllCategoriesQuery();
                var result = await mediator.Send(query);

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("GetAllCategories")
            .WithSummary("Gets all categories")
            .WithDescription("Retrieves all available product categories")
            .Produces<List<CategoryDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);

            // Create a new category
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.IsSuccess
                    ? Results.Created($"/api/categories/{result.Data}", result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("CreateCategory")
            .WithSummary("Creates a new category")
            .WithDescription("Creates a new product category")
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}