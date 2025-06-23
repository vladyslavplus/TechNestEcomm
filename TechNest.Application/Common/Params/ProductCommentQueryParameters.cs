namespace TechNest.Application.Common.Params;

public class ProductCommentQueryParameters : QueryParameters
{
    public Guid? ProductId { get; set; }
    public Guid? UserId { get; set; }
    public string? Text { get; set; }
}