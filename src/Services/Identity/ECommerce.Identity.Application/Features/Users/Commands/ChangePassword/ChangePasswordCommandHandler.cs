using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Services;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, Result>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHashingService passwordHashingService;
        public ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordHashingService passwordHashingService)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.passwordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
        }
        public async Task<Result> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return Result.Failure("Kullanıcı bulunamadı");
            }
            if (!passwordHashingService.VerifyPassword(user.PasswordHash,request.CurrentPassword))
            {
                return Result.Failure("Mevcut şifre hatalı");
            }
            if (request.NewPassword != request.ConfirmPassword)
            {
                return Result.Failure("Yeni şifre ve onay şifresi eşleşmiyor");
            }
            var newPasswordHash = passwordHashingService.HashPassword(request.NewPassword);
            user.ChangePassword(newPasswordHash);
            await userRepository.UpdateAsync(user, cancellationToken);
            return Result.Success();
        }  

    }
}
