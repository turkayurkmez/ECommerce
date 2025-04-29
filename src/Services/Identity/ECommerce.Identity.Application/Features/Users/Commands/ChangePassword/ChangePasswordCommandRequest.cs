using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Commands.ChangePassword
{
    public record ChangePasswordCommandRequest : IRequest<Result>
    {
        public Guid UserId { get; init; }
        public string CurrentPassword { get; init; } = string.Empty;
        public string NewPassword { get; init; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
    }
}
