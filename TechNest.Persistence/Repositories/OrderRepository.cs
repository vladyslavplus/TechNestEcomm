using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Domain.Entities.Enums;
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
            query = query.Where(o => o.FullName.ToLower().Contains(parameters.FullName.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.City))
            query = query.Where(o => o.City.ToLower().Contains(parameters.City.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.Email))
            query = query.Where(o => o.Email.ToLower().Contains(parameters.Email.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.Phone))
            query = query.Where(o => o.Phone.ToLower().Contains(parameters.Phone.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.Department))
            query = query.Where(o => o.Department.ToLower().Contains(parameters.Department.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.Status)
            && Enum.TryParse<OrderStatus>(parameters.Status, true, out var parsedStatus))
        {
            query = query.Where(o => o.Status == parsedStatus);
        }

        if (parameters.CreatedFrom.HasValue)
            query = query.Where(o => o.CreatedAt >= parameters.CreatedFrom.Value);

        if (parameters.CreatedTo.HasValue)
            query = query.Where(o => o.CreatedAt <= parameters.CreatedTo.Value);

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
    
    public async Task<Order?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}