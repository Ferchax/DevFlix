using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;
using DevFlix.Api.Repositories;

namespace DevFlix.Api.Services;

public class VideoService(IVideoRepository videoRepository, IChannelRepository channelRepository) : IVideoService
{
    public Task<IEnumerable<VideoDto>> GetAllAsync(int? categoryId = null, int? channelId = null)
    {
        return videoRepository.GetAllAsync(categoryId, channelId);
    }

    public async Task<VideoDto?> GetByIdAsync(int id)
    {
        var video = await videoRepository.GetByIdAsync(id);
        if (video is null)
        {
            return null;
        }

        return new VideoDto
        {
            Id = video.Id,
            YouTubeVideoId = video.YouTubeVideoId,
            Title = video.Title,
            Url = video.Url,
            ThumbnailUrl = video.ThumbnailUrl,
            PublishedAt = video.PublishedAt,
            ChannelId = video.ChannelId,
            Channel = video.Channel != null ? new ChannelDto
            {
                Id = video.Channel.Id,
                Name = video.Channel.Name,
                YouTubeChannelId = video.Channel.YouTubeChannelId,
                FeedUrl = video.Channel.FeedUrl,
                CategoryId = video.Channel.CategoryId,
                IsActive = video.Channel.IsActive
            } : null
        };
    }

    public async Task<VideoDto> CreateAsync(CreateVideoDto dto)
    {
        var channel = await channelRepository.GetByIdAsync(dto.ChannelId);
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

        await videoRepository.AddAsync(video);

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
        var video = await videoRepository.GetByIdAsync(id);
        if (video is null)
        {
            return null;
        }

        var channel = await channelRepository.GetByIdAsync(dto.ChannelId);
        if (channel is null)
        {
            throw new InvalidOperationException($"Channel with ID {dto.ChannelId} not found.");
        }

        video.Title = dto.Title;
        video.Url = dto.Url;
        video.ThumbnailUrl = dto.ThumbnailUrl;
        video.PublishedAt = dto.PublishedAt;
        video.ChannelId = dto.ChannelId;

        await videoRepository.UpdateAsync(video);

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
        var video = await videoRepository.GetByIdAsync(id);
        if (video is null)
        {
            return false;
        }

        await videoRepository.RemoveAsync(video);

        return true;
    }
}
