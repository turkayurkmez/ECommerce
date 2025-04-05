using Mapster;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MapsterMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Catalog.Domain.Entities;

namespace Catalog.Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(int Id, string Name, string Description) : IRequest<Result>;
    
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(request.Id);
            
            if (existingCategory == null)
            {
                return Result.Failure($"Category with id {request.Id} not found.");
            }

            existingCategory.UpdateBasicInfo(request.Name, request.Description);


            var category = _mapper.Map<Category>(request);

            await _categoryRepository.UpdateAsync(existingCategory);
            
            return Result.Success();
        }


    }

    
}