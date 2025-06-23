namespace TechNest.Application.Common.Params;

public class CartItemQueryParameters : QueryParameters
{
    public Guid? UserId { get; set; }
    public Guid? ProductId { get; set; }
}