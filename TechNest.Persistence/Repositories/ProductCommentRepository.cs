using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class ProductCommentRepository(ApplicationDbContext context) : GenericRepository<ProductComment>(context), IProductCommentRepository
{
    public async Task<PagedList<ProductComment>> GetAllPaginatedAsync(ProductCommentQueryParameters parameters, ISortHelper<ProductComment> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (parameters.ProductId.HasValue)
            query = query.Where(pc => pc.ProductId == parameters.ProductId.Value);

        if (parameters.UserId.HasValue)
            query = query.Where(pc => pc.UserId == parameters.UserId.Value);

        if (!string.IsNullOrWhiteSpace(parameters.Text))
            query = query.Where(pc => pc.Text.Contains(parameters.Text, StringComparison.CurrentCultureIgnoreCase));

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(pc => pc.Product)
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
}