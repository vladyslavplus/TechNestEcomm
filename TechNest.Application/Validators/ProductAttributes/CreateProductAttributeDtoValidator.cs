using FluentValidation;
using TechNest.Application.DTOs.ProductAttributes;

namespace TechNest.Application.Validators.ProductAttributes;

public class CreateProductAttributeDtoValidator : AbstractValidator<CreateProductAttributeDto>
{
    public CreateProductAttributeDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Key).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Value).NotEmpty().MaximumLength(500);
    }
}