using FluentValidation;
using TechNest.Application.DTOs.Categories;

namespace TechNest.Application.Validators.Categories;

public class CreateCategoryDtoValidator :  AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}