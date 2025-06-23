namespace TechNest.Application.Common.Params;

public class ProductRatingQueryParameters : QueryParameters
{
    public Guid? ProductId { get; set; }
    public Guid? UserId { get; set; }
    public int? MinValue { get; set; }
    public int? MaxValue { get; set; }
}