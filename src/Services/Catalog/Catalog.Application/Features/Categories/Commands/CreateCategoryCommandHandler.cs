using MediatR;
using ECommerce.Catalog.Domain.Repositories;
using MapsterMapper;
using ECommerce.Catalog.Domain.Entities;
using ECommerce.Common.Results;

namespace Catalog.Application.Features.Categories.Commands
{
    public record CreateCategoryCommand : IRequest<Result<int>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public bool IsActive { get; init; }
        public int? ParentCategoryId { get; init; }
        public int Level { get; init; } = 1;
    }

    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<CreateCategoryCommand, Result<int>>
    {
        // Dependencies would be injected here (e.g., repository)
        
       
        public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check if the parent category exists if ParentCategoryId is provided
            if (request.ParentCategoryId.HasValue)
            {
                var parentCategoryExists = await categoryRepository.ExistsByIdAsync(request.ParentCategoryId.Value, cancellationToken);
                if (!parentCategoryExists)
                {
                    return Result<int>.Failure($"Parent category with ID {request.ParentCategoryId} does not exist.");
                }
            }          

            var category = mapper.Map<Category>(request);
            await categoryRepository.AddAsync(category, cancellationToken);

            // Placeholder implementation
            return Result<int>.Success(category.Id);   
        }
    }
}