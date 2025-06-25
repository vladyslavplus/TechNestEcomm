using FluentValidation;
using TechNest.Application.DTOs.CartItems;

namespace TechNest.Application.Validators.CartItems;

public class CreateCartItemDtoValidator : AbstractValidator<CreateCartItemDto>
{
    public CreateCartItemDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}