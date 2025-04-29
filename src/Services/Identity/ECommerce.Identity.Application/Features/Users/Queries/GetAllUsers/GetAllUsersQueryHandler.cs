using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using ECommerce.Identity.Domain.Repositories;
using Mapster;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, Result<List<UserDto>>>
    {
        private readonly IUserRepository userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<Result<List<UserDto>>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            // Implementation of the query handler

            var skip = (request.PageNumber - 1) * request.PageSize;
            var users = await userRepository.GetAllAsync(skip, request.PageSize, cancellationToken);
            if (users == null || users.Count == 0)
            {
                return Result<List<UserDto>>.Failure("No users found");
            }
            var usersDtos = users.Adapt<List<UserDto>>();
            return Result<List<UserDto>>.Success(usersDtos);
        }
    }
}
