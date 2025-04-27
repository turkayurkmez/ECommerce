using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand : IRequest<Result<AuthResponseDto>>
    {
        public string AccessToken { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty;
        public string IpAddress { get; init; } = string.Empty;
    }
}
