using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<PagedList<Category>> GetAllPaginatedAsync(CategoryQueryParameters parameters, ISortHelper<Category> sortHelper, CancellationToken cancellationToken = default);
}