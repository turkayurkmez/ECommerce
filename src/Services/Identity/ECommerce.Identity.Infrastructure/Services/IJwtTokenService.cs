using ECommerce.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Identity.Infrastructure.Services
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(User user, IList<string> roles);
        string GenerateRefreshToken();
        (ClaimsPrincipal, JwtSecurityToken) ValidateJwtToken(string token);
    }
}
