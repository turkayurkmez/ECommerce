using FluentValidation;

namespace ECommerce.Identity.Application.Features.Auth.Commands.Login
{
    public class LoginCommandRequestValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş olamaz");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre boş olamaz");

        }
    }

}
