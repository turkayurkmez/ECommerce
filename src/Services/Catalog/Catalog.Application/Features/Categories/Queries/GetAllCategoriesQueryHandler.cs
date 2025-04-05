using Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MapsterMapper;
using MediatR;

namespace Catalog.Application.Features.Categories.Queries;

 public record GetAllCategoriesQuery : IRequest<Result<List<CategoryDto>>>;
public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryDto>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public Task<Result<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetAllAsync(cancellationToken);
        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        
        return Task.FromResult(Result<List<CategoryDto>>.Success(categoryDtos));
    }
}
