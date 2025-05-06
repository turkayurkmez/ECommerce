using ECommerce.Identity.Application.Features.Roles.Commands.CreateRole;
using ECommerce.Identity.Application.Features.Roles.Queries.GetAllRoles;
using MediatR;

namespace ECommerce.Identity.API.Endpoints
{
    public static class RoleEndpoints
    {
        public static RouteGroupBuilder MapRoleEndpoints(this RouteGroupBuilder group)
        {
            // Rol listeleme endpoint'i
            group.MapGet("/", async (
                [AsParameters] GetAllRolesQueryRequest query,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(query, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("GetAllRoles")
            .WithSummary("Rolleri listeler")
            .WithDescription("Sistemde tanımlı tüm rolleri listeler.")
            .RequireAuthorization("Admin");

            // Rol oluşturma endpoint'i
            group.MapPost("/", async (
                CreateRoleCommandRequest request,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(request, cancellationToken);
                return result.IsSuccess
                    ? Results.Created($"/api/v1/roles/{result.Data.Id}", result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("CreateRole")
            .WithSummary("Yeni rol oluşturur")
            .WithDescription("Sistemde yeni bir rol tanımlar.")
            .RequireAuthorization("Admin");

            return group;
        }
    }
}
