using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;

namespace  Catalog.Application.Features.Categories.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<Result>;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingCategory == null)
        {
            return Result.Failure($"{request.Id} id'li kategori bulunamadı.");
        }

        await _categoryRepository.DeleteAsync(existingCategory, cancellationToken);

        return Result.Success();
    }
}
