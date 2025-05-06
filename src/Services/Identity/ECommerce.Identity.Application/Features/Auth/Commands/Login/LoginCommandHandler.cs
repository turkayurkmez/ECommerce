using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Services;
using Mapster;
using MediatR;
using Microsoft.Extensions.Options;

namespace ECommerce.Identity.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, Result<AuthResponseDto>>
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IPasswordHashingService passwordHashingService;
        private readonly JwtTokenSettings jwtTokenSettings;

        // Constructor injection with IOptions
        public LoginCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService, IPasswordHashingService passwordHashingService, IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            this.passwordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
            this.jwtTokenSettings = jwtTokenSettings.Value;
        }


        public async Task<Result<AuthResponseDto>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            //kullanıcı adına göre kullanıcıyı al
            var user = await userRepository.GetByUserNameAsync(request.UserName, cancellationToken);
            if (user == null)
            {
                return Result<AuthResponseDto>.Failure("Kullanıcı adı veya şifre hatalı");
            }

            //kullanıcı aktif mi
            if (!user.IsActive)
            {
                return Result<AuthResponseDto>.Failure("Kullanıcı aktif değil");
            }
            //şifreyi kontrol et
            if (!passwordHashingService.VerifyPassword(user.PasswordHash,request.Password))
            {
                return Result<AuthResponseDto>.Failure("Kullanıcı adı veya şifre hatalı");
            }

            //rollerle birlikte kullanıcıyı al
            var userWithRoles = await userRepository.GetWithRolesAsync(user.Id, cancellationToken);
            var roles = userWithRoles?.Roles.Select(r => r.Role.Name).ToList();
            //jwt token oluştur
            var accessToken = jwtTokenService.GenerateJwtToken(user, roles);
            var refreshToken = jwtTokenService.GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddDays(jwtTokenSettings.RefreshTokenExpirationDays);
            //refresh token kaydet:
            user.AddRefreshToken(refreshToken, expiresAt, request.IpAddress);
            user.UpdateLastLoginDate();
            await userRepository.UpdateAsync(user, cancellationToken);

            var authResponse = new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
                User = user.Adapt<UserDto>()
            };

            authResponse.User.Roles = roles;
            return Result<AuthResponseDto>.Success(authResponse);


        }
    }

}
