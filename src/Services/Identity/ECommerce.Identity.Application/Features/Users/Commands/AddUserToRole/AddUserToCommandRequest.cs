using ECommerce.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Users.Commands.AddUserToRole
{
    public record AddUserToCommandRequest : IRequest<Result>
    {
        public Guid UserId { get; init; }
        public string RoleName { get; init; } = string.Empty;
    }
}
