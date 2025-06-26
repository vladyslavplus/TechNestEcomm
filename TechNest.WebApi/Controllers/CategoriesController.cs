using Microsoft.AspNetCore.Mvc;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Categories;
using TechNest.Application.Interfaces.Services;

namespace TechNest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<CategoryDto>>> GetAll([FromQuery] CategoryQueryParameters parameters, CancellationToken cancellationToken)
    {
        var categories = await categoryService.GetAllAsync(parameters, cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var category = await categoryService.GetByIdAsync(id, cancellationToken);
        if (category == null)
            return NotFound();
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await categoryService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CategoryDto>> Update(Guid id, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != dto.Id)
            return BadRequest("ID mismatch");

        var updated = await categoryService.UpdateAsync(dto, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await categoryService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}