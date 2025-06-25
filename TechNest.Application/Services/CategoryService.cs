using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Categories;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class CategoryService(IUnitOfWork unitOfWork, ISortHelper<Category> sortHelper) : ICategoryService
{
    public async Task<PagedList<CategoryDto>> GetAllAsync(CategoryQueryParameters parameters, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.Categories.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return categories.Adapt<PagedList<CategoryDto>>();
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        return category?.Adapt<CategoryDto>();
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<Category>();
        await unitOfWork.Categories.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Adapt<CategoryDto>();
    }

    public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(dto.Id, cancellationToken);
        if (category is null) throw new Exception("Category not found");

        dto.Adapt(category);
        unitOfWork.Categories.Update(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Adapt<CategoryDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        if (category is null) throw new Exception("Category not found");

        unitOfWork.Categories.Delete(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}