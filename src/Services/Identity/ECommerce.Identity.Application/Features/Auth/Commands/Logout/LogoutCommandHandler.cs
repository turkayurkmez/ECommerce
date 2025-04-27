using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
    {
        private readonly IRefreshTokenRepository tokenRepository;
        private readonly IUserRepository userRepository;
        public LogoutCommandHandler(IRefreshTokenRepository tokenRepository, IUserRepository userRepository)
        {
            this.tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await tokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
            if (refreshToken == null)
            {
                return Result.Failure("Refresh token bulunamadı");
            }

            var user = await userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);
            if (user == null)
            {
                return Result.Failure("Kullanıcı bulunamadı");
            }
            user.RevokeRefreshToken(refreshToken.Token);
            await userRepository.UpdateAsync(user, cancellationToken);  
            return Result.Success();
        }
    }
   
}
