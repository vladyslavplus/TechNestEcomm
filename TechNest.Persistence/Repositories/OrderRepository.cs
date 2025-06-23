using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IOrderRepository
{
    public async Task<PagedList<Order>> GetAllPaginatedAsync(OrderQueryParameters parameters, ISortHelper<Order> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (parameters.UserId.HasValue)
            query = query.Where(o => o.UserId == parameters.UserId);

        if (!string.IsNullOrWhiteSpace(parameters.FullName))
            query = query.Where(o => o.FullName.Contains(parameters.FullName, StringComparison.CurrentCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(parameters.City))
            query = query.Where(o => o.City.Contains(parameters.City, StringComparison.CurrentCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(parameters.Email))
            query = query.Where(o => o.Email.Contains(parameters.Email, StringComparison.CurrentCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(parameters.Phone))
            query = query.Where(o => o.Phone.Contains(parameters.Phone, StringComparison.CurrentCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(parameters.Department))
            query = query.Where(o => o.Department.Contains(parameters.Department, StringComparison.CurrentCultureIgnoreCase));

        if (parameters.Status.HasValue)
            query = query.Where(o => o.Status == parameters.Status.Value);

        if (parameters.CreatedFrom.HasValue)
            query = query.Where(o => o.CreatedAt >= parameters.CreatedFrom.Value);

        if (parameters.CreatedTo.HasValue)
            query = query.Where(o => o.CreatedAt <= parameters.CreatedTo.Value);

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(o => o.Items)
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
}