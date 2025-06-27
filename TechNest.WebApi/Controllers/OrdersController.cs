using Microsoft.AspNetCore.Mvc;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Orders;
using TechNest.Application.Interfaces.Services;

namespace TechNest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<OrderDto>>> GetAll([FromQuery] OrderQueryParameters parameters, CancellationToken cancellationToken)
    {
        var orders = await orderService.GetAllAsync(parameters, cancellationToken);
        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderService.GetByIdAsync(id, cancellationToken);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await orderService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<OrderDto>> Update(Guid id, [FromBody] UpdateOrderDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id) return BadRequest("Mismatched IDs");

        var updated = await orderService.UpdateAsync(dto, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await orderService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}