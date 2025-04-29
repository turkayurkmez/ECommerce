using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Queries.GetUsersById
{
    public record GetUserByIdQueryRequest(Guid Id) : IRequest<Result<UserDto>>;

}
