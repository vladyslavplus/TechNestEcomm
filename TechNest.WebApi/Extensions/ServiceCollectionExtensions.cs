using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace TechNest.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        
        return services;
    }
}