using FluentValidation;
using TechNest.Application.DTOs.Products;

namespace TechNest.Application.Validators.Products;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        Include(new CreateProductDtoValidator());
        RuleFor(x => x.Id).NotEmpty();
    }
}