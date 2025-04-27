using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string Username { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public bool IsActive { get; init; }
        public bool EmailConfirmed { get; init; }
        public DateTime? LastLoginDate { get; init; }
        public List<string> Roles { get; set; } = new List<string>();
    }
    public record RegisterUserCommandRequest : IRequest<Result<UserDto>>
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;

    }
}
