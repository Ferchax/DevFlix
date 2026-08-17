using DevFlix.Api.DTOs;
using FluentValidation;

namespace DevFlix.Api.Validators;

public class CreateVideoValidator : AbstractValidator<CreateVideoDto>
{
    public CreateVideoValidator()
    {
        RuleFor(v => v.YouTubeVideoId)
            .NotEmpty()
            .MaximumLength(50);

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
