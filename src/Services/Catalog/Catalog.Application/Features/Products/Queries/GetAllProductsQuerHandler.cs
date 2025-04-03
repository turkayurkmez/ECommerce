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
    public record GetAllProductsQuery(PaginationParameters Pagination) : IRequest<Result<List<ProductSummaryDto>>>;
    public class GetAllProductsQuerHandler : IRequestHandler<GetAllProductsQuery, Result<List<ProductSummaryDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQuerHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductSummaryDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.Pagination.PageNumber - 1) * request.Pagination.PageSize;
            var take = request.Pagination.PageSize;
            var products = await _productRepository.GetAllAsync(skip,take,cancellationToken);

            if (products == null || !products.Any())
            {
                return Result<List<ProductSummaryDto>>.Success(new List<ProductSummaryDto>());
            }
            var productDtos = _mapper.Map<List<ProductSummaryDto>>(products);
            return Result<List<ProductSummaryDto>>.Success(productDtos);



        }
    }
}
