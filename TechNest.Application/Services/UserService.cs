using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Users;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class UserService(IUnitOfWork unitOfWork, ISortHelper<User> sortHelper) : IUserService
{
    public async Task<PagedList<UserDto>> GetAllAsync(UserQueryParameters parameters, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.Users.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return users.Adapt<PagedList<UserDto>>();
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        return user?.Adapt<UserDto>();
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<User>();
        await unitOfWork.Users.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Adapt<UserDto>();
    }

    public async Task<UserDto> UpdateAsync(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(dto.Id, cancellationToken);
        if (user is null) throw new KeyNotFoundException("User not found");

        dto.Adapt(user);
        unitOfWork.Users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Adapt<UserDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        if (user is null) throw new KeyNotFoundException("User not found");

        unitOfWork.Users.Delete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}