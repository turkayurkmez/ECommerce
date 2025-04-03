using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Models;
using ECommerce.Common.Results;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Features.Products.Queries
{
    public record GetProductsByBrandQuery(int BrandId, PaginationParameters PaginationParameters) : IRequest<Result<List<ProductSummaryDto>>>;

    public class GetProductsByBrandQueryHandler(IProductRepository _productRepository, IMapper _mapper)
    {
       
        public async Task<Result<List<ProductSummaryDto>>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.PaginationParameters.PageNumber - 1) * request.PaginationParameters.PageSize;
            var take = request.PaginationParameters.PageSize;
            var products = await _productRepository.GetProductsByBrandAsync(request.BrandId, skip, take, cancellationToken);
            if (products == null || !products.Any())
            {
                return Result<List<ProductSummaryDto>>.Success(new List<ProductSummaryDto>());
            }
            var productDtos = _mapper.Map<List<ProductSummaryDto>>(products);
            return Result<List<ProductSummaryDto>>.Success(productDtos);
        }
    }
}
