using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<PagedList<User>> GetAllPaginatedAsync(UserQueryParameters parameters, ISortHelper<User> sortHelper, CancellationToken cancellationToken = default);
}