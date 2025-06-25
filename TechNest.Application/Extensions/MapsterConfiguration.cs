using System.Reflection;
using Mapster;

namespace TechNest.Application.Extensions;

public static class MapsterConfiguration
{
    public static void RegisterMapsterMappings()
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}