using FluentValidation;

namespace ECommerce.Identity.Application.Features.Users.Commands.AddUserToRole
{
    public class AddUserToCommandRequestValidator : AbstractValidator<AddUserToCommandRequest>
    {
        public AddUserToCommandRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Kullanıcı Id boş olamaz");
            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage("Rol adı boş olamaz");
        }
    }
}
