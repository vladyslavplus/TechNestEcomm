using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Products;

namespace TechNest.Application.Interfaces.Services;

public interface IProductService
{
    Task<PagedList<ProductDto>> GetAllAsync(ProductQueryParameters parameters, CancellationToken cancellationToken);
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken);
    Task<ProductDto> UpdateAsync(UpdateProductDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}