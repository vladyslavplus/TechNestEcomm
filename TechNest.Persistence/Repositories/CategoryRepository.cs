using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class CategoryRepository(ApplicationDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public async Task<PagedList<Category>> GetAllPaginatedAsync(CategoryQueryParameters parameters, ISortHelper<Category> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(c => c.Name.Contains(parameters.Name, StringComparison.CurrentCultureIgnoreCase));

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
}