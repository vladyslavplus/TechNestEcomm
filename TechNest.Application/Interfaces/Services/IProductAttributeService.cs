using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.ProductAttributes;

namespace TechNest.Application.Interfaces.Services;

public interface IProductAttributeService
{
    Task<PagedList<ProductAttributeDto>> GetAllAsync(ProductAttributeQueryParameters parameters, CancellationToken cancellationToken);
    Task<ProductAttributeDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductAttributeDto> CreateAsync(CreateProductAttributeDto dto, CancellationToken cancellationToken);
    Task<ProductAttributeDto> UpdateAsync(UpdateProductAttributeDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}