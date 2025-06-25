using FluentValidation;
using TechNest.Application.DTOs.ProductAttributes;

namespace TechNest.Application.Validators.ProductAttributes;

public class UpdateProductAttributeDtoValidator : AbstractValidator<UpdateProductAttributeDto>
{
    public UpdateProductAttributeDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Key).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Value).NotEmpty().MaximumLength(500);
    }
}