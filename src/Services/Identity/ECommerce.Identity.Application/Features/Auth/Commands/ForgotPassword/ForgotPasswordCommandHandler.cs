using ECommerce.Common.Results;
using ECommerce.EventBus.Events.Notification;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Services;
using ECommerce.MessageBroker.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace ECommerce.Identity.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMessagePublisher _messagePublisher;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;

        public ForgotPasswordCommandHandler(IUserRepository userRepository, IPasswordHashingService passwordHashingService, IJwtTokenService jwtTokenService, IMessagePublisher messagePublisher, ILogger<ForgotPasswordCommandHandler> logger)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
            _jwtTokenService = jwtTokenService;
            _messagePublisher = messagePublisher;
            _logger = logger;

        }



        public async Task<Result> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            // Implement your logic to handle the forgot password request here
            // For example, send a password reset email to the user
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                _logger.LogWarning("Şifre sıfırlama talebi için kullanıcı bulunamadı: {Email}", request.Email);
                return Result.Failure("Kullanıcı bulunamadı");
            }

            // Check if the user is active
            if (!user.IsActive)
            {
                _logger.LogWarning("Şifre sıfırlama talebi için kullanıcı aktif değil: {Email}", request.Email);
                return Result.Failure("Kullanıcı aktif değil");
            }


            var resetToken = generateResetToken();

            try
            {
                var resetLink = $"https://sample.com/reset-password?email={Uri.EscapeDataString(request.Email)}&token={Uri.EscapeDataString(resetToken)}";

                var emailEvent = new SendEmailEvent
                {
                    To = request.Email,
                    Subject = "Şifre Sıfırlama Talebi",
                    Body = $"<p>Şifre sıfırlama talebiniz alınmıştır. Lütfen aşağıdaki linke tıklayarak şifrenizi sıfırlayın:</p><p><a href=\"{resetLink}\">Şifre Sıfırlama Linki</a></p>",
                    IsHtml = true

                };

                await _messagePublisher.PublishAsync(emailEvent, cancellationToken);
                _logger.LogInformation("Şifre sıfırlama talebi için e-posta gönderildi: {Email}", request.Email);
                return Result.Success("Şifre sıfırlama talebi başarıyla gönderildi.");

            }
            catch (Exception)
            {

                _logger.LogError("Şifre sıfırlama talebi için e-posta gönderilemedi: {Email}", request.Email);
                return Result.Failure("Şifre sıfırlama talebi gönderilirken bir hata oluştu.");

            }


            // Generate a password reset token
           


          
        }

        private string generateResetToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

}
