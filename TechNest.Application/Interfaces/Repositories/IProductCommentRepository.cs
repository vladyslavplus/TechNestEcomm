using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface IProductCommentRepository : IGenericRepository<ProductComment>
{
    Task<PagedList<ProductComment>> GetAllPaginatedAsync(ProductCommentQueryParameters parameters, ISortHelper<ProductComment> sortHelper, CancellationToken cancellationToken = default);
}