using ECommerce.Basket.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Features.Commands
{
    public record DeleteBasketCommand(string UserId) : IRequest<Result>;
    internal class DeleteBasketCommandHandler
    {
        private readonly IBasketRepository _basketRepository;
        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<Result> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            await _basketRepository.DeleteBasketAsync(request.UserId, cancellationToken);
           return Result.Success("Sepet başarıyla silindi");
        }
    }
}
