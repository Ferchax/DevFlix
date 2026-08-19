using DevFlix.Api.DTOs;
using FluentValidation;

namespace DevFlix.Api.Validators;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(120);
    }
}
