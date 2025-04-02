using Catalog.Application.DTOs;
using Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products
{
    public record UpdateProductCommand(UpdateProductDto ProductDto) : IRequest<Result>;
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {

        private readonly IProductRepository _productRepository;
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ProductDto;
            var product = await _productRepository.GetByIdAsync(dto.Id, cancellationToken);
            if (product == null)
            {
                return Result.Failure($"Ürün bulunamadı");
            }

            product.UpdateBasicInfo(dto.Name, dto.Description,dto.SKU);
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
