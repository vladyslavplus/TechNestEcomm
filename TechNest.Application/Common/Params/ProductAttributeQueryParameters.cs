namespace TechNest.Application.Common.Params;

public class ProductAttributeQueryParameters : QueryParameters
{
    public Guid? ProductId { get; set; }
    public string? Key { get; set; }
    public string? Value { get; set; }
}