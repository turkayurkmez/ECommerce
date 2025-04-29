using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using ECommerce.Identity.Domain.Repositories;
using Mapster;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, Result<UserDto>>
    {
        private readonly IUserRepository userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<Result<UserDto>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return Result<UserDto>.Failure("Kullanıcı bulunamadı");
            }
            user.UpdateProfile(request.FirstName, request.LastName);
            user.ChangeEmail(request.Email);

            await userRepository.UpdateAsync(user, cancellationToken);
            return Result<UserDto>.Success(user.Adapt<UserDto>());
        }
    }
}
