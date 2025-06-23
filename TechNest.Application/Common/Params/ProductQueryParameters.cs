namespace TechNest.Application.Common.Params;

public class ProductQueryParameters : QueryParameters
{
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }
    public Guid? CategoryId { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public int? MinStock { get; set; }
    public int? MaxStock { get; set; }
}