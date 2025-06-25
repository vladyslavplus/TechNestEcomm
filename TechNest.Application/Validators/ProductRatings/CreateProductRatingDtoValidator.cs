using FluentValidation;
using TechNest.Application.DTOs.ProductRatings;

namespace TechNest.Application.Validators.ProductRatings;

public class CreateProductRatingDtoValidator : AbstractValidator<CreateProductRatingDto>
{
    public CreateProductRatingDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Value).InclusiveBetween(1, 5);
    }
}