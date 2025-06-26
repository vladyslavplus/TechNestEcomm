using Microsoft.AspNetCore.Mvc;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.OrderItems;
using TechNest.Application.Interfaces.Services;

namespace TechNest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController(IOrderItemService orderItemService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<OrderItemDto>>> GetAll([FromQuery] OrderItemQueryParameters parameters, CancellationToken cancellationToken)
    {
        var items = await orderItemService.GetAllAsync(parameters, cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderItemDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await orderItemService.GetByIdAsync(id, cancellationToken);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItemDto>> Create([FromBody] CreateOrderItemDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await orderItemService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<OrderItemDto>> Update(Guid id, [FromBody] UpdateOrderItemDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id) return BadRequest("Mismatched IDs");
        var updated = await orderItemService.UpdateAsync(dto, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await orderItemService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}