using FluentValidation;
using TechNest.Application.DTOs.Orders;

namespace TechNest.Application.Validators.Orders;

public class UpdateOrderDtoValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Status).IsInEnum();
    }
}