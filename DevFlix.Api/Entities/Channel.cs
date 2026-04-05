namespace DevFlix.Api.Entities;

public class Channel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string YouTubeChannelId { get; set; } = string.Empty;

    public string FeedUrl { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public bool IsActive { get; set; } = true;

    public Category? Category { get; set; }

    public ICollection<Video> Videos { get; set; } = new List<Video>();
}
