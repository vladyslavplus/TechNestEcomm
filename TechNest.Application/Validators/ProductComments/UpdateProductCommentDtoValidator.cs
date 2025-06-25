using FluentValidation;
using TechNest.Application.DTOs.ProductComments;

namespace TechNest.Application.Validators.ProductComments;

public class UpdateProductCommentDtoValidator : AbstractValidator<UpdateProductCommentDto>
{
    public UpdateProductCommentDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(1000);
    }
}