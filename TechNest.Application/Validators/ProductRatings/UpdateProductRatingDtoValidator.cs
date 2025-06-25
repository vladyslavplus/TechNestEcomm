using FluentValidation;
using TechNest.Application.DTOs.ProductRatings;

namespace TechNest.Application.Validators.ProductRatings;

public class UpdateProductRatingDtoValidator : AbstractValidator<UpdateProductRatingDto>
{
    public UpdateProductRatingDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Value).InclusiveBetween(1, 5);
    }
}