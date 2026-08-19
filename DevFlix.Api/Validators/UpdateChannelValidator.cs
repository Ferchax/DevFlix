using DevFlix.Api.DTOs;
using FluentValidation;

namespace DevFlix.Api.Validators;

public class UpdateChannelValidator : AbstractValidator<UpdateChannelDto>
{
    public UpdateChannelValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.YouTubeChannelId)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.FeedUrl)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(v => v.CategoryId)
            .GreaterThan(0);
    }
}
