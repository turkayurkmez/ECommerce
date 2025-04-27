using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Users.Commands
{
    public record ChangePasswordCommandRequest : IRequest<Result>
    {
        public Guid UserId { get; init; }
        public string CurrentPassword { get; init; } = string.Empty;
        public string NewPassword { get; init; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
    }

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
            if (!passwordHashingService.VerifyPassword(request.CurrentPassword, user.PasswordHash))
            {
                return Result.Failure("Mevcut şifre hatalı");
            }
            if (request.NewPassword != request.ConfirmPassword)
            {
                return Result.Failure("Yeni şifre ve onay şifresi eşleşmiyor");
            }
            user.ChangePassword(passwordHashingService.HashPassword(request.NewPassword));
            await userRepository.UpdateAsync(user, cancellationToken);
            return Result.Success();
        }  

    }
}
