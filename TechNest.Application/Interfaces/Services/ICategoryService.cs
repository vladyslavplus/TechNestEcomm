using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Categories;

namespace TechNest.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<PagedList<CategoryDto>> GetAllAsync(CategoryQueryParameters parameters, CancellationToken cancellationToken);
    Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken);
    Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}