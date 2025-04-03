using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Repositories;
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
    public record GetProductByIdQuery(int Id) : IRequest<Result<ProductDto>>;
    public class GetProductByIdQueryHandler 
    {
        //IProductRepository, IMapper enjekte edilecek
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                return Result<ProductDto>.Failure($"Ürün bulunamadı. ID: {request.Id}");
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Success(productDto);
        }
    }
}
