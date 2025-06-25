using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.ProductComments;

namespace TechNest.Application.Interfaces.Services;

public interface IProductCommentService
{
    Task<PagedList<ProductCommentDto>> GetAllAsync(ProductCommentQueryParameters parameters, CancellationToken cancellationToken);
    Task<ProductCommentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductCommentDto> CreateAsync(CreateProductCommentDto dto, CancellationToken cancellationToken);
    Task<ProductCommentDto> UpdateAsync(UpdateProductCommentDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}