using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Queries.GetAllUsers
{
    public record GetAllUsersQueryRequest : IRequest<Result<List<UserDto>>>
    {
        //PageNumber
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        //PageSize

    }
}
