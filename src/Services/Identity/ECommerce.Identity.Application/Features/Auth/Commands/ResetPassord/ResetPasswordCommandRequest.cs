using ECommerce.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Auth.Commands.ResetPassord
{
    public record ResetPasswordCommandRequest(
        string Email,
        string Token,
        string NewPassword,
        string ConfirmPassword
        ) : IRequest<Result>;

}
