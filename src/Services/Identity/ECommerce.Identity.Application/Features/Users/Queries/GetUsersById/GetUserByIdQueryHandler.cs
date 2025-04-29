using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using ECommerce.Identity.Domain.Repositories;
using Mapster;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Queries.GetUsersById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, Result<UserDto>>
    {
        private readonly IUserRepository userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<Result<UserDto>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result<UserDto>.Failure("Kullanıcı bulunamadı");
            }
            return Result<UserDto>.Success(user.Adapt<UserDto>());
        }
    }

}
