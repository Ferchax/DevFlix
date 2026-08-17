namespace DevFlix.Api.DTOs;

public class UpdateVideoDto
{
    public string Title { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string? ThumbnailUrl { get; set; }

    public DateTimeOffset PublishedAt { get; set; }

    public int ChannelId { get; set; }
}
