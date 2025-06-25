using FluentValidation;
using TechNest.Application.DTOs.CartItems;

namespace TechNest.Application.Validators.CartItems;

public class UpdateCartItemDtoValidator : AbstractValidator<UpdateCartItemDto>
{
    public UpdateCartItemDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}