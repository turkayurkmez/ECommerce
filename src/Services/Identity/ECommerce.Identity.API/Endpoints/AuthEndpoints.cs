using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Auth.Commands.ForgotPassword;
using ECommerce.Identity.Application.Features.Auth.Commands.Login;
using ECommerce.Identity.Application.Features.Auth.Commands.Logout;
using ECommerce.Identity.Application.Features.Auth.Commands.RefreshToken;
using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using ECommerce.Identity.Application.Features.Auth.Commands.ResetPassord;
using MediatR;

namespace ECommerce.Identity.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
        {

            //register - yeni kullanıcı kaydı

            group.MapPost("/register", async (RegisterUserCommandRequest command, ISender mediator, CancellationToken token) =>
            {
                var result = await mediator.Send(command, token);
                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result.Errors);


            })
            .WithName("RegisterUser")
            .WithSummary("Register a new user")
            .WithDescription("This endpoint allows you to register a new user in the system. You need to provide a username, password, email, first name, and last name.")
            .AllowAnonymous()
            .Produces<Result<UserDto>>(StatusCodes.Status200OK)
            .Produces<Result<UserDto>>(StatusCodes.Status400BadRequest);

            //login - kullanıcı girişi
            group.MapPost("/login", async (LoginCommandRequest command, HttpContext httpContext, ISender mediator, CancellationToken cancellationToken) =>
            {
                command = command with
                {
                    IpAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unkown"

                };

                var result = await mediator.Send(command, cancellationToken);
                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result.Errors);
            })
                .WithName("LoginUser")
                .WithSummary("Login a user")
                .WithDescription("This endpoint allows you to login a user in the system. You need to provide a username and password.")
                .AllowAnonymous();

          

            group.MapPost("/refresh-token", async (
               RefreshTokenCommand command,
              HttpContext httpContext,
              ISender mediator,
              CancellationToken cancellationToken) =>
            {
                // IP adresini ekleyelim
                command = command with { IpAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown" };

                var result = await mediator.Send(command, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
          .WithName("RefreshToken")
          .WithSummary("Refresh access token")
          .WithDescription("Refreshes an expired access token using a valid refresh token")
          .AllowAnonymous();

            group.MapPost("/logout", async (
               LogoutCommand command,
               ISender mediator,
               CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
           .WithName("Logout")
           .WithSummary("Logout user")
           .WithDescription("Logs out the user by invalidating the refresh token")
           .RequireAuthorization();

            // Forgot Password - Şifre sıfırlama e-postası gönderme
            group.MapPost("/forgot-password", async (
                ForgotPasswordCommandRequest command,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("ForgotPassword")
            .WithSummary("Request password reset")
            .WithDescription("Sends a password reset email to the user's email address")
            .AllowAnonymous();

            group.MapPost("/reset-password", async (
                 ResetPasswordCommandRequest command,
                ISender mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Errors);
            })
            .WithName("ResetPassword")
            .WithSummary("Reset password")
            .WithDescription("Resets the user's password using a valid reset token")
            .AllowAnonymous();

            return group;
        }
    }
}
