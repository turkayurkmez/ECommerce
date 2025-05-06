using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Services;
using MediatR;

namespace ECommerce.Identity.Application.Features.Auth.Commands.ResetPassord
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;

        public ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordHashingService passwordHashingService)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<Result> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            // Kullanıcıyı email ile bul
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                return Result.Failure("Kullanıcı bulunamadı");
            }
            // Token doğrulama işlemi burada yapılmalı
            // Örnek: var isValidToken = await _userRepository.ValidateResetTokenAsync(user.Id, request.Token, cancellationToken);
            // if (!isValidToken)
            // {
            //     return Result.Failure("Geçersiz token");
            // }
            // Şifreyi güncelle

            if (request.NewPassword != request.ConfirmPassword)
            {
                return Result.Failure("Yeni şifre ve onay şifresi  eşleşmiyor ");
            }

            if (string.IsNullOrEmpty(request.Token))
            {
                return Result.Failure("Geçersiz veya süresi dolmuş token.");
            }

            // Şifreyi hashleme ve kullanıcıya atama
            var newPasswordHash = _passwordHashingService.HashPassword(request.NewPassword);
            user.ChangePassword(newPasswordHash);


            foreach (var refreshToken in user.RefreshTokens)
            {
                user.RevokeRefreshToken(refreshToken.Token);
                
            }

            await _userRepository.UpdateAsync(user, cancellationToken);


            return Result.Success("Şifre başarıyla güncellendi");

        }
    }

}
