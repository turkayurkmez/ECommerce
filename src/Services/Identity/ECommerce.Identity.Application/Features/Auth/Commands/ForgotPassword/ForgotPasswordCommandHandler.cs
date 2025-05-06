using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Services;
using MediatR;

namespace ECommerce.Identity.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IJwtTokenService _jwtTokenService;

        public ForgotPasswordCommandHandler(IUserRepository userRepository, IPasswordHashingService passwordHashingService, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
            _jwtTokenService = jwtTokenService;
        }



        public async Task<Result> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            // Implement your logic to handle the forgot password request here
            // For example, send a password reset email to the user
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                return Result.Failure("Kullanıcı bulunamadı");
            }

            // Generate a password reset token
            user.RevokeRefreshToken(user!.RefreshTokens.FirstOrDefault()?.Token);


            return Result.Success("Şifre sıfırlama talebi başarıyla gönderildi.");
        }
    }

}
