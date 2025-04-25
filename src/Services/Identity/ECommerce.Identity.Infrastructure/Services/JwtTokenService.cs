using ECommerce.Identity.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Identity.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtTokenSettings _jwtTokenSettings;
        public JwtTokenService(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            _jwtTokenSettings = jwtTokenSettings.Value;
        }
        public string GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //name -> userName
                new Claim(ClaimTypes.Name, user.UserName),

            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtTokenSettings.Issuer,
                audience: _jwtTokenSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtTokenSettings.AccessTokenExpirationMinutes),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public (ClaimsPrincipal, JwtSecurityToken) ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.Secret)),
                ValidateIssuer = true,
                ValidIssuer = _jwtTokenSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtTokenSettings.Audience,
                ValidateLifetime = true
            };
            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                return (principal, jwtToken);

            }
            catch (Exception)
            {

                return (null, null);
            }
        }

    }
}
