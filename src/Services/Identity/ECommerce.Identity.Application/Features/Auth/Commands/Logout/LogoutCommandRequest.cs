using ECommerce.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Auth.Commands.Logout
{
    public record LogoutCommand : IRequest<Result>
    {
        public string RefreshToken { get; init; } = string.Empty;
    }
}
