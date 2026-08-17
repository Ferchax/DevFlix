using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;

namespace DevFlix.Api.Repositories;

public interface IVideoRepository
{
    Task<IEnumerable<VideoDto>> GetAllAsync(int? categoryId = null, int? channelId = null);

    Task<Video?> GetByIdAsync(int id);

    Task AddAsync(Video video);

    Task RemoveAsync(Video video);

    Task UpdateAsync(Video video);
}
