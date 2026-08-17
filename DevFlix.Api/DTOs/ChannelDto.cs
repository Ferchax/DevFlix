namespace DevFlix.Api.DTOs;

public class ChannelDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string YouTubeChannelId { get; set; } = string.Empty;

    public string FeedUrl { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public bool IsActive { get; set; }
}
