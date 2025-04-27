using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.Login;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Services;
using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
    {
        private readonly IRefreshTokenRepository tokenRepository;
        private readonly IJwtTokenService jwtTokenService;
        private readonly JwtTokenSettings jwtTokenSettings;
        private readonly IUserRepository userRepository;

        public RefreshTokenCommandHandler(IRefreshTokenRepository tokenRepository,  IJwtTokenService jwtTokenService, IOptions<JwtTokenSettings> jwtTokenSettings, IUserRepository userRepository)
        {
            this.tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
            this.jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            this.jwtTokenSettings = jwtTokenSettings.Value;
            this.userRepository = userRepository;
        }


        public async Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            // Refresh token'ı veritabanında kontrol et
            var (principal, jwtToken) = jwtTokenService.ValidateJwtToken(request.AccessToken);
            if (principal == null || jwtToken == null)
            {
                return Result<AuthResponseDto>.Failure("Geçersiz token");
            }

            //tokenin süresi dolmuş mu kontrol et

            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return Result<AuthResponseDto>.Failure("Token süresi dolmamış");

            }

            // Kullanıcıyı al
            var userIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value,out var userId))
            {
                return Result<AuthResponseDto>.Failure("Kullanıcı bulunamadı");
            }

            var storedToken = await tokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
            if (storedToken == null)
            {
                return Result<AuthResponseDto>.Failure("Refresh token bulunamadı");
            }

            if (storedToken.UserId != userId)
            {
                return Result<AuthResponseDto>.Failure("Refresh token kullanıcıya ait değil");

            }

            // Kullanıcıyı veritabanından roller ile birlikte al
            var user = await userRepository.GetWithRolesAsync(userId, cancellationToken);
            if (user == null || !user.IsActive)
            {
                return Result<AuthResponseDto>.Failure("Kullanıcı bulunamadı");
            }

            // Yeni access token ve refresh token oluştur
            var roles = user.Roles.Select(r => r.Role.Name).ToList();
            var newAccessToken = jwtTokenService.GenerateJwtToken(user, roles);
            var newRefreshToken = jwtTokenService.GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddDays(jwtTokenSettings.RefreshTokenExpirationDays);
            // Yeni refresh token'ı veritabanına kaydet
            user.AddRefreshToken(newRefreshToken, expiresAt, request.IpAddress);
            user.UpdateLastLoginDate();
            await userRepository.UpdateAsync(user, cancellationToken);
            // Eski refresh token'ı iptal et
            var authResponse = new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = expiresAt,
                User = user.Adapt<UserDto>()
            };

            authResponse.User.Roles = roles;
            return Result<AuthResponseDto>.Success(authResponse);







        }
    }
   
}
