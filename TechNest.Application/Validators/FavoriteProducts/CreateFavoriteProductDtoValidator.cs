using FluentValidation;
using TechNest.Application.DTOs.FavoriteProducts;

namespace TechNest.Application.Validators.FavoriteProducts;

public class CreateFavoriteProductDtoValidator : AbstractValidator<CreateFavoriteProductDto>
{
    public CreateFavoriteProductDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
    }
}