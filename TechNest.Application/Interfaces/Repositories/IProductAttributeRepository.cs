using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface IProductAttributeRepository : IGenericRepository<ProductAttribute>
{
    Task<PagedList<ProductAttribute>> GetAllPaginatedAsync(ProductAttributeQueryParameters parameters, ISortHelper<ProductAttribute> sortHelper, CancellationToken cancellationToken = default);
}