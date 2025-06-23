using Microsoft.Extensions.DependencyInjection;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Persistence.Helpers;
using TechNest.Persistence.Repositories;

namespace TechNest.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped(typeof(ISortHelper<>), typeof(SortHelper<>));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
        services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
        services.AddScoped<IProductRatingRepository, ProductRatingRepository>();

        return services;
    }
}