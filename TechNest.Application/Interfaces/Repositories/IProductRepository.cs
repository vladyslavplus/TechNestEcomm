using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<PagedList<Product>> GetAllPaginatedAsync(ProductQueryParameters parameters, ISortHelper<Product> sortHelper, CancellationToken cancellationToken = default);
}