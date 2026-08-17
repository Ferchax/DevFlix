using DevFlix.Api.Data;
using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFlix.Api.Repositories;

public class VideoRepository(DevFlixDbContext context) : IVideoRepository
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

    public async Task<Video?> GetByIdAsync(int id)
    {
        return await context.Videos.FindAsync(id);
    }

    public async Task AddAsync(Video video)
    {
        context.Videos.Add(video);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Video video)
    {
        context.Videos.Remove(video);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Video video)
    {
        context.Videos.Update(video);
        await context.SaveChangesAsync();
    }
}
