using Microsoft.AspNetCore.Mvc;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.CartItems;
using TechNest.Application.Interfaces.Services;

namespace TechNest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemsController(ICartItemService cartItemService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<CartItemDto>>> GetAll([FromQuery] CartItemQueryParameters parameters, CancellationToken cancellationToken)
    {
        var items = await cartItemService.GetAllAsync(parameters, cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CartItemDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await cartItemService.GetByIdAsync(id, cancellationToken);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<CartItemDto>> Create([FromBody] CreateCartItemDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await cartItemService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CartItemDto>> Update(Guid id, [FromBody] UpdateCartItemDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != dto.Id)
            return BadRequest("ID mismatch");

        var updated = await cartItemService.UpdateAsync(dto, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await cartItemService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}