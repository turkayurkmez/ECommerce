using Catalog.Application.DTOs;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands
{

    public record CreateProductCommand(CreateProductDto ProductDto) : IRequest<Result<int>>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
    {
        //IProductRepository, IMapper, ICategroyRepository, IBrandRepository enjekte edilecek

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }



        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
          var dto = request.ProductDto;
            var categoryExists = await _categoryRepository.ExistsByIdAsync(dto.CategoryId, cancellationToken);
            if (!categoryExists)
            {
                return Result<int>.Failure($"Kategori bulunamadı");

            }

            var brandExists = await _brandRepository.ExistsByIdAsync(dto.BrandId, cancellationToken);
            if (!brandExists)
            {
                return Result<int>.Failure($"Marka bulunamadı");
            }

            var product = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(product, cancellationToken);
            return Result<int>.Success(product.Id);

        }
    }
}
