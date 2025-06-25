using FluentValidation;
using TechNest.Application.DTOs.Categories;

namespace TechNest.Application.Validators.Categories;

public class UpdateCategoryDtoValidator :  AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}