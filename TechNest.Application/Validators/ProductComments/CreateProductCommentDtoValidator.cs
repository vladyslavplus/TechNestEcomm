using FluentValidation;
using TechNest.Application.DTOs.ProductComments;

namespace TechNest.Application.Validators.ProductComments;

public class CreateProductCommentDtoValidator : AbstractValidator<CreateProductCommentDto>
{
    public CreateProductCommentDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(1000);
    }
}