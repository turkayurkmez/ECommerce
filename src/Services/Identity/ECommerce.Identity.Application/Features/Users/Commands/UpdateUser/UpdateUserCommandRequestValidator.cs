using FluentValidation;

namespace ECommerce.Identity.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandRequestValidator : AbstractValidator<UpdateUserCommandRequest>
    {
        public UpdateUserCommandRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı ID boş olamaz");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad boş olamaz");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş olamaz");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Geçersiz e-posta adresi");
        }
    }
}
