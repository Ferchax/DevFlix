using DevFlix.Api.DTOs;
using FluentValidation;

namespace DevFlix.Api.Validators;

public class UpdateVideoValidator : AbstractValidator<UpdateVideoDto>
{
    public UpdateVideoValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(v => v.Url)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(v => v.ThumbnailUrl)
            .MaximumLength(500);

        RuleFor(v => v.PublishedAt)
            .NotEmpty();

        RuleFor(v => v.ChannelId)
            .GreaterThan(0);
    }
}
