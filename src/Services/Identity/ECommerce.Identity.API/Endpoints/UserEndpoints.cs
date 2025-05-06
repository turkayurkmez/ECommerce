using ECommerce.Identity.Application.Features.Users.Commands.AddUserToRole;
using ECommerce.Identity.Application.Features.Users.Commands.ChangePassword;
using ECommerce.Identity.Application.Features.Users.Commands.DeleteUser;
using ECommerce.Identity.Application.Features.Users.Commands.UpdateUser;
using ECommerce.Identity.Application.Features.Users.Queries.GetAllUsers;
using ECommerce.Identity.Application.Features.Users.Queries.GetUsersById;
using MediatR;

namespace ECommerce.Identity.API.Endpoints
{
    public static class UserEndpoints
    {
        public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
        {
            // Kullanıcı listeleme endpoint'i
            group.MapGet("/", async (
                [AsParameters] GetAllUsersQueryRequest query,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(query, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("GetAllUsers")
            .WithSummary("Kullanıcıları listeler")
            .WithDescription("Sayfalama parametrelerine göre tüm kullanıcıları listeler.")
            .RequireAuthorization("Admin");

            // Kullanıcı detayı endpoint'i
            group.MapGet("/{id:guid}", async (
                Guid id,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GetUserByIdQueryRequest(id), cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.NotFound(result.Errors);
            })
            .WithName("GetUserById")
            .WithSummary("Kullanıcı detayını getirir")
            .WithDescription("Belirtilen ID'ye sahip kullanıcının detaylarını getirir.")
            .RequireAuthorization();

            // Kullanıcı güncelleme endpoint'i
            group.MapPut("/{userId:guid}", async (
                Guid userId,
                UpdateUserCommandRequest request,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                // UserId'yi route'dan alıp request nesnesine ekleyerek yeni bir komut oluşturuyoruz
                request = request with { UserId = userId };

                var result = await mediator.Send(request, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("UpdateUser")
            .WithSummary("Kullanıcıyı günceller")
            .WithDescription("Belirtilen ID'ye sahip kullanıcının bilgilerini günceller.")
            .RequireAuthorization();

            // Kullanıcı silme endpoint'i
            group.MapDelete("/{id:guid}", async (
                Guid id,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new DeleteUserCommandRequest(id), cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("DeleteUser")
            .WithSummary("Kullanıcıyı siler")
            .WithDescription("Belirtilen ID'ye sahip kullanıcıyı siler.")
            .RequireAuthorization("Admin");

            // Kullanıcı şifre değiştirme endpoint'i
            group.MapPost("/{userId:guid}/change-password", async (
                Guid userId,
                ChangePasswordCommandRequest request,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                // UserId'yi route'dan alıp request nesnesine ekleyerek yeni bir komut oluşturuyoruz
                request = request with { UserId = userId };

                var result = await mediator.Send(request, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("ChangePassword")
            .WithSummary("Kullanıcı şifresini değiştirir")
            .WithDescription("Kullanıcının kendi şifresini değiştirmesini sağlar.")
            .RequireAuthorization();

            // Kullanıcıya rol atama endpoint'i
            group.MapPost("/{userId:guid}/roles", async (
                Guid userId,
                AddUserToCommandRequest request,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                // UserId'yi route'dan alıp request nesnesine ekleyerek yeni bir komut oluşturuyoruz
                request = request with { UserId = userId };

                var result = await mediator.Send(request, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("AddUserToRole")
            .WithSummary("Kullanıcıya rol atar")
            .WithDescription("Belirtilen kullanıcıya belirtilen rolü atar.")
            .RequireAuthorization("Admin");

            return group;
        }
    }
}
