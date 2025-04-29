using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommandRequest(Guid Id) : IRequest<Result>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, Result>
    {
        private readonly IUserRepository userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<Result> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result.Failure("Kullanıcı bulunamadı");
            }
            await userRepository.DeleteAsync(user, cancellationToken);
            return Result.Success();

        }
    }
    

}
