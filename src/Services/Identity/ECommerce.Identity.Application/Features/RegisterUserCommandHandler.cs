using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Entities;
using ECommerce.Identity.Domain.Repositories;
using ECommerce.Identity.Infrastructure.Services;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IPasswordHashingService passwordHashingService;

        public RegisterUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHashingService passwordHashingService)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            this.passwordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
        }
        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcı adı eşsiz mi?
            if (await userRepository.IsUserNameInUniqueAsync(request.Username,cancellationToken))
            {
                return Result<UserDto>.Failure($"Kullanıcı adı {request.Username} zaten mevcut");

            }

            // E-posta adresi eşsiz mi?
            if (await userRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
            {
                return Result<UserDto>.Failure($"E-posta adresi {request.Email} zaten mevcut");
            }

            var passwordHash = passwordHashingService.HashPassword(request.Password);

            var user = new User(request.Username, request.Email, request.FirstName, request.LastName, passwordHash);
            var defaultRoles = await roleRepository.GetDefaultRolesAsync(cancellationToken);
            if (defaultRoles != null && defaultRoles.Count > 0)
            {
                foreach (var role in defaultRoles)
                {
                    user.AddRole(role);
                }
            }

            await userRepository.AddAsync(user, cancellationToken);
            var userDto = user.Adapt<UserDto>();
            userDto.Roles = defaultRoles.Select(r => r.Name).ToList();

            return Result<UserDto>.Success(userDto);



        }
    }
    
}
