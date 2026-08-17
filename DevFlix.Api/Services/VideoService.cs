using DevFlix.Api.Data;
using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFlix.Api.Services;

public class VideoService(DevFlixDbContext context) : IVideoService
{
    public async Task<IEnumerable<VideoDto>> GetAllAsync(int? categoryId = null, int? channelId = null)
    {
        var query = context.Videos
            .Include(v => v.Channel)
            .ThenInclude(c => c!.Category)
            .AsQueryable();

        if (channelId.HasValue)
        {
            query = query.Where(v => v.ChannelId == channelId.Value);
        }

        if (categoryId.HasValue)
        {
            query = query.Where(v => v.Channel!.CategoryId == categoryId.Value);
        }

        return await query
            .Select(v => new VideoDto
            {
                Id = v.Id,
                YouTubeVideoId = v.YouTubeVideoId,
                Title = v.Title,
                Url = v.Url,
                ThumbnailUrl = v.ThumbnailUrl,
                PublishedAt = v.PublishedAt,
                ChannelId = v.ChannelId,
                Channel = v.Channel != null ? new ChannelDto
                {
                    Id = v.Channel.Id,
                    Name = v.Channel.Name,
                    YouTubeChannelId = v.Channel.YouTubeChannelId,
                    FeedUrl = v.Channel.FeedUrl,
                    CategoryId = v.Channel.CategoryId,
                    IsActive = v.Channel.IsActive
                } : null
            })
            .ToListAsync();
    }

    public async Task<VideoDto?> GetByIdAsync(int id)
    {
        var video = await context.Videos
            .Include(v => v.Channel)
            .ThenInclude(c => c!.Category)
            .Where(v => v.Id == id)
            .Select(v => new VideoDto
            {
                Id = v.Id,
                YouTubeVideoId = v.YouTubeVideoId,
                Title = v.Title,
                Url = v.Url,
                ThumbnailUrl = v.ThumbnailUrl,
                PublishedAt = v.PublishedAt,
                ChannelId = v.ChannelId,
                Channel = v.Channel != null ? new ChannelDto
                {
                    Id = v.Channel.Id,
                    Name = v.Channel.Name,
                    YouTubeChannelId = v.Channel.YouTubeChannelId,
                    FeedUrl = v.Channel.FeedUrl,
                    CategoryId = v.Channel.CategoryId,
                    IsActive = v.Channel.IsActive
                } : null
            })
            .FirstOrDefaultAsync();

        return video;
    }

    public async Task<VideoDto> CreateAsync(CreateVideoDto dto)
    {
        var channel = await context.Channels.FindAsync(dto.ChannelId);
        if (channel is null)
        {
            throw new InvalidOperationException($"Channel with ID {dto.ChannelId} not found.");
        }

        var video = new Video
        {
            YouTubeVideoId = dto.YouTubeVideoId,
            Title = dto.Title,
            Url = dto.Url,
            ThumbnailUrl = dto.ThumbnailUrl,
            PublishedAt = dto.PublishedAt,
            ChannelId = dto.ChannelId
        };

        context.Videos.Add(video);
        await context.SaveChangesAsync();

        return new VideoDto
        {
            Id = video.Id,
            YouTubeVideoId = video.YouTubeVideoId,
            Title = video.Title,
            Url = video.Url,
            ThumbnailUrl = video.ThumbnailUrl,
            PublishedAt = video.PublishedAt,
            ChannelId = video.ChannelId,
            Channel = new ChannelDto
            {
                Id = channel.Id,
                Name = channel.Name,
                YouTubeChannelId = channel.YouTubeChannelId,
                FeedUrl = channel.FeedUrl,
                CategoryId = channel.CategoryId,
                IsActive = channel.IsActive
            }
        };
    }

    public async Task<VideoDto?> UpdateAsync(int id, UpdateVideoDto dto)
    {
        var video = await context.Videos.FindAsync(id);
        if (video is null)
        {
            return null;
        }

        var channel = await context.Channels.FindAsync(dto.ChannelId);
        if (channel is null)
        {
            throw new InvalidOperationException($"Channel with ID {dto.ChannelId} not found.");
        }

        video.Title = dto.Title;
        video.Url = dto.Url;
        video.ThumbnailUrl = dto.ThumbnailUrl;
        video.PublishedAt = dto.PublishedAt;
        video.ChannelId = dto.ChannelId;

        await context.SaveChangesAsync();

        return new VideoDto
        {
            Id = video.Id,
            YouTubeVideoId = video.YouTubeVideoId,
            Title = video.Title,
            Url = video.Url,
            ThumbnailUrl = video.ThumbnailUrl,
            PublishedAt = video.PublishedAt,
            ChannelId = video.ChannelId,
            Channel = new ChannelDto
            {
                Id = channel.Id,
                Name = channel.Name,
                YouTubeChannelId = channel.YouTubeChannelId,
                FeedUrl = channel.FeedUrl,
                CategoryId = channel.CategoryId,
                IsActive = channel.IsActive
            }
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var video = await context.Videos.FindAsync(id);
        if (video is null)
        {
            return false;
        }

        context.Videos.Remove(video);
        await context.SaveChangesAsync();

        return true;
    }
}
