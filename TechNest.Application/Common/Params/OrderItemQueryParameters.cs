namespace TechNest.Application.Common.Params;

public class OrderItemQueryParameters : QueryParameters
{
    public Guid? OrderId { get; set; }
    public Guid? ProductId { get; set; }

    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }

    public decimal? MinUnitPrice { get; set; }
    public decimal? MaxUnitPrice { get; set; }
}