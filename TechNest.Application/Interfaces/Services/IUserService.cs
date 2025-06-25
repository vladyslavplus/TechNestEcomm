using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Users;

namespace TechNest.Application.Interfaces.Services;

public interface IUserService
{
    Task<PagedList<UserDto>> GetAllAsync(UserQueryParameters parameters, CancellationToken cancellationToken);
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken);
    Task<UserDto> UpdateAsync(UpdateUserDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}