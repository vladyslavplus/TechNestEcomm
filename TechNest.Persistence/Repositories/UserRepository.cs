﻿using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<PagedList<User>> GetAllPaginatedAsync(UserQueryParameters parameters, ISortHelper<User> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.FullName))
            query = query.Where(u => u.FullName.ToLower().Contains(parameters.FullName.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.City))
            query = query.Where(u => u.City != null && u.City.ToLower().Contains(parameters.City.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.Email))
            query = query.Where(u => u.Email.ToLower().Contains(parameters.Email.ToLower()));

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query.AsNoTracking().ToPagedListAsync(
            parameters.PageNumber,
            parameters.PageSize,
            cancellationToken
        );
    }
}