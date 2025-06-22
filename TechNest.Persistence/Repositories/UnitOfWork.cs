using TechNest.Application.Interfaces.Repositories;
using TechNest.Persistence.Data;

namespace TechNest.Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = [];

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out var repo))
            return (IGenericRepository<T>)repo;

        var newRepo = new GenericRepository<T>(context);
        _repositories[typeof(T)] = newRepo;
        return newRepo;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);

    public async ValueTask DisposeAsync() => await context.DisposeAsync();
}