using Microsoft.AspNetCore.Mvc;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.FavoriteProducts;
using TechNest.Application.Interfaces.Services;

namespace TechNest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteProductsController(IFavoriteProductService favoriteProductService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<FavoriteProductDto>>> GetAll([FromQuery] FavoriteProductQueryParameters parameters, CancellationToken cancellationToken)
    {
        var favorites = await favoriteProductService.GetAllAsync(parameters, cancellationToken);
        return Ok(favorites);
    }
    
    [HttpGet("by-user-product")]
    public async Task<ActionResult<FavoriteProductDto>> GetByUserIdAndProductId([FromQuery] Guid userId, [FromQuery] Guid productId, CancellationToken cancellationToken)
    {
        var favorite = await favoriteProductService.GetByUserIdAndProductIdAsync(userId, productId, cancellationToken);
        if (favorite == null) return NotFound();
        return Ok(favorite);
    }

    [HttpPost]
    public async Task<ActionResult<FavoriteProductDto>> Create([FromBody] CreateFavoriteProductDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await favoriteProductService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetByUserIdAndProductId), new { userId = created.UserId, productId = created.Product.Id }, created);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid userId, [FromQuery] Guid productId, CancellationToken cancellationToken)
    {
        await favoriteProductService.DeleteAsync(userId, productId, cancellationToken);
        return NoContent();
    }
}