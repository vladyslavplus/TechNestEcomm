using Microsoft.AspNetCore.Mvc;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.ProductAttributes;
using TechNest.Application.Interfaces.Services;

namespace TechNest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductAttributesController(IProductAttributeService productAttributeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<ProductAttributeDto>>> GetAll([FromQuery] ProductAttributeQueryParameters parameters, CancellationToken cancellationToken)
    {
        var attributes = await productAttributeService.GetAllAsync(parameters, cancellationToken);
        return Ok(attributes);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductAttributeDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var attribute = await productAttributeService.GetByIdAsync(id, cancellationToken);
        if (attribute == null) return NotFound();
        return Ok(attribute);
    }

    [HttpPost]
    public async Task<ActionResult<ProductAttributeDto>> Create([FromBody] CreateProductAttributeDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await productAttributeService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductAttributeDto>> Update(Guid id, [FromBody] UpdateProductAttributeDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id) return BadRequest("Mismatched IDs");

        var updated = await productAttributeService.UpdateAsync(dto, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await productAttributeService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}