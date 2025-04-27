using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandRequestValidator : AbstractValidator<RegisterUserCommandRequest>
    {
        public RegisterUserCommandRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş olamaz")
                .MinimumLength(3)
                .WithMessage("Kullanıcı adı en az 3 karakter olmalıdır")
                .MaximumLength(50)
                .WithMessage("Kullanıcı adı en fazla 20 karakter olmalıdır");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email boş olamaz")
                .EmailAddress()
                .WithMessage("Geçersiz email adresi");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre boş olamaz")
                .MinimumLength(6)
                .WithMessage("Şifre en az 6 karakter olmalıdır")
                .MaximumLength(20)
                .WithMessage("Şifre en fazla 20 karakter olmalıdır");
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("İsim boş olamaz")
                .MinimumLength(2)
                .WithMessage("İsim en az 2 karakter olmalıdır")
                .MaximumLength(50)
                .WithMessage("İsim en fazla 50 karakter olmalıdır");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyisim boş olamaz")
                .MinimumLength(2)
                .WithMessage("Soyisim en az 2 karakter olmalıdır")
                .MaximumLength(50)
                .WithMessage("Soyisim en fazla 50 karakter olmalıdır");
        }
    }
  
}
