using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TechNest.Application.Interfaces.Services;
using TechNest.Application.Services;

namespace TechNest.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IFavoriteProductService, FavoriteProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductAttributeService, ProductAttributeService>();
        services.AddScoped<IProductCommentService, ProductCommentService>();
        services.AddScoped<IProductRatingService, ProductRatingService>();

        return services;
    }
}