using Microsoft.Extensions.DependencyInjection;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Persistence.Repositories;

namespace TechNest.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}