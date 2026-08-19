using DevFlix.Api.Data;
using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFlix.Api.Repositories;

public class ChannelRepository(DevFlixDbContext context) : IChannelRepository
{
    public async Task<IEnumerable<ChannelDto>> GetAllAsync()
    {
        return await context.Channels
            .Include(c => c.Category)
            .Select(c => new ChannelDto
            {
                Id = c.Id,
                Name = c.Name,
                YouTubeChannelId = c.YouTubeChannelId,
                FeedUrl = c.FeedUrl,
                CategoryId = c.CategoryId,
                IsActive = c.IsActive,
                Category = c.Category != null ? new CategoryDto
                {
                    Id = c.Category.Id,
                    Name = c.Category.Name
                } : null
            })
            .ToListAsync();
    }

    public async Task<Channel?> GetByIdAsync(int id)
    {
        return await context.Channels.FindAsync(id);
    }

    public async Task<ChannelDto?> GetByIdWithCategoryAsync(int id)
    {
        return await context.Channels
            .Include(c => c.Category)
            .Where(c => c.Id == id)
            .Select(c => new ChannelDto
            {
                Id = c.Id,
                Name = c.Name,
                YouTubeChannelId = c.YouTubeChannelId,
                FeedUrl = c.FeedUrl,
                CategoryId = c.CategoryId,
                IsActive = c.IsActive,
                Category = c.Category != null ? new CategoryDto
                {
                    Id = c.Category.Id,
                    Name = c.Category.Name
                } : null
            })
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Channel channel)
    {
        context.Channels.Add(channel);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Channel channel)
    {
        context.Channels.Update(channel);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Channel channel)
    {
        context.Channels.Remove(channel);
        await context.SaveChangesAsync();
    }

    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await context.Categories.AnyAsync(c => c.Id == categoryId);
    }
}
