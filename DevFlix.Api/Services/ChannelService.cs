using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;
using DevFlix.Api.Repositories;

namespace DevFlix.Api.Services;

public class ChannelService(IChannelRepository channelRepository) : IChannelService
{
    public Task<IEnumerable<ChannelDto>> GetAllAsync()
    {
        return channelRepository.GetAllAsync();
    }

    public async Task<ChannelDto?> GetByIdAsync(int id)
    {
        return await channelRepository.GetByIdWithCategoryAsync(id);
    }

    public async Task<ChannelDto> CreateAsync(CreateChannelDto dto)
    {
        var categoryExists = await channelRepository.CategoryExistsAsync(dto.CategoryId);
        if (!categoryExists)
        {
            throw new InvalidOperationException($"Category with ID {dto.CategoryId} not found.");
        }

        var channel = new Channel
        {
            Name = dto.Name,
            YouTubeChannelId = dto.YouTubeChannelId,
            FeedUrl = dto.FeedUrl,
            CategoryId = dto.CategoryId,
            IsActive = dto.IsActive
        };

        await channelRepository.AddAsync(channel);

        return new ChannelDto
        {
            Id = channel.Id,
            Name = channel.Name,
            YouTubeChannelId = channel.YouTubeChannelId,
            FeedUrl = channel.FeedUrl,
            CategoryId = channel.CategoryId,
            IsActive = channel.IsActive,
            Category = new CategoryDto
            {
                Id = dto.CategoryId,
                Name = string.Empty
            }
        };
    }

    public async Task<ChannelDto?> UpdateAsync(int id, UpdateChannelDto dto)
    {
        var channel = await channelRepository.GetByIdAsync(id);
        if (channel is null)
        {
            return null;
        }

        var categoryExists = await channelRepository.CategoryExistsAsync(dto.CategoryId);
        if (!categoryExists)
        {
            throw new InvalidOperationException($"Category with ID {dto.CategoryId} not found.");
        }

        channel.Name = dto.Name;
        channel.YouTubeChannelId = dto.YouTubeChannelId;
        channel.FeedUrl = dto.FeedUrl;
        channel.CategoryId = dto.CategoryId;
        channel.IsActive = dto.IsActive;

        await channelRepository.UpdateAsync(channel);

        return new ChannelDto
        {
            Id = channel.Id,
            Name = channel.Name,
            YouTubeChannelId = channel.YouTubeChannelId,
            FeedUrl = channel.FeedUrl,
            CategoryId = channel.CategoryId,
            IsActive = channel.IsActive,
            Category = new CategoryDto
            {
                Id = dto.CategoryId,
                Name = string.Empty
            }
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var channel = await channelRepository.GetByIdAsync(id);
        if (channel is null)
        {
            return false;
        }

        await channelRepository.RemoveAsync(channel);

        return true;
    }
}
