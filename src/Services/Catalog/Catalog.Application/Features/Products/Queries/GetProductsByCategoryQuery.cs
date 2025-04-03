using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Models;
using ECommerce.Common.Results;
using MapsterMapper;
using MediatR;

namespace ECommerce.Catalog.Application.Features.Products.Queries
{

    public record GetProductsByCategoryQuery(int CategoryId, PaginationParameters PaginationParameters) : IRequest<Result<List<ProductSummaryDto>>>;
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, Result<List<ProductSummaryDto>>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public async Task<Result<List<ProductSummaryDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {

            //Burada category id ye göre ürünleri getirme işlemi yapılacak

            var skip = (request.PaginationParameters.PageNumber - 1) * request.PaginationParameters.PageSize;
            var take = request.PaginationParameters.PageSize;
            var products = await _productRepository.GetProductsByCategoryAsync(request.CategoryId, skip, take, cancellationToken);

            //eğer products null ise veya products içerisinde eleman yok ise boş bir liste döndürülecek
            if (products == null || !products.Any())
            {
                return Result<List<ProductSummaryDto>>.Success(new List<ProductSummaryDto>());
            }
            var productDtos = _mapper.Map<List<ProductSummaryDto>>(products);
            return Result<List<ProductSummaryDto>>.Success(productDtos);



        }
    }
}
