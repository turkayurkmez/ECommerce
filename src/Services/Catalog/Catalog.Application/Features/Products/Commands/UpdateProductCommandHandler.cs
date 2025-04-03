using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Features.Products.Commands
{
    public record UpdateProductCommand(UpdateProductDto ProductDto) : IRequest<Result>;
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {

        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ProductDto;
            var product = await _productRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (product == null)
            {
                return Result.Failure($"Ürün bulunamadı");
            }

            product.UpdateBasicInfo(dto.Name, dto.Description, dto.SKU);
            product.ChangePrice(dto.Price);
            product.ChangeStock(dto.StockQuantity);
            if (product.CategoryId != dto.CategoryId)
            {
                product.ChangeCategory(dto.CategoryId);

            }

            if (product.BrandId != dto.BrandId)
            {
                product.ChangeBrand(dto.BrandId);
            }

            await _productRepository.UpdateAsync(product, cancellationToken);
            var result = Result.Success("Ürün başarıyla güncellendi");
            return result;

        }
    }
}
