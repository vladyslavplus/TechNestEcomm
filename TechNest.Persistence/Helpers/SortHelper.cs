using System.Linq.Dynamic.Core;
using System.Reflection;
using TechNest.Application.Common.Interfaces.Helpers;

namespace TechNest.Persistence.Helpers;

public class SortHelper<T> : ISortHelper<T>
{
    public IQueryable<T> ApplySort(IQueryable<T> entities, string? orderByQueryString)
    {
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            var idProp = propertyInfos.FirstOrDefault(p =>
                p.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase));

            return idProp != null ? entities.OrderBy("Id") : entities;
        }

        var orderParams = orderByQueryString.Trim().Split(',');

        var orderQuery = string.Join(",", orderParams.Select(param =>
        {
            var trimmedParam = param.Trim();
            var orderDescending = trimmedParam.EndsWith(" desc", StringComparison.InvariantCultureIgnoreCase);
            var propertyName = orderDescending ? trimmedParam[..^5].Trim() : trimmedParam;

            var property = propertyInfos.FirstOrDefault(pi =>
                pi.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

            return property != null
                ? $"{property.Name} {(orderDescending ? "descending" : "ascending")}"
                : null;
        }).Where(x => x != null));

        return string.IsNullOrWhiteSpace(orderQuery)
            ? entities.OrderBy("Id") 
            : entities.OrderBy(orderQuery);
    }
}