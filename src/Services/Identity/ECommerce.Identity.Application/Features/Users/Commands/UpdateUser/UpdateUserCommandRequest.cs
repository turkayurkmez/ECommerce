using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommandRequest : IRequest<Result<UserDto>>
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;


    }
}
