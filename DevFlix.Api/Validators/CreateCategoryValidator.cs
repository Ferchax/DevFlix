using DevFlix.Api.DTOs;
using FluentValidation;

namespace DevFlix.Api.Validators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(120);
    }
}
