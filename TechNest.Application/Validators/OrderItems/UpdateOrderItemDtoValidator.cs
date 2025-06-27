using FluentValidation;
using TechNest.Application.DTOs.OrderItems;

namespace TechNest.Application.Validators.OrderItems;

public class UpdateOrderItemDtoValidator : AbstractValidator<UpdateOrderItemDto>
{
    public UpdateOrderItemDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}