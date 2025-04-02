using Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands
{
    public record DeleteProductCommand(int Id) : IRequest<Result>;
    public class DeleteProductComanndHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductComanndHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                return Result.Failure($"Ürün bulunamadı. ID: {request.Id}");
            }

            product.Delete();
            await _productRepository.UpdateAsync(product, cancellationToken);
            return Result.Success("Ürün başarıyla silindi");

        }
    }
}
