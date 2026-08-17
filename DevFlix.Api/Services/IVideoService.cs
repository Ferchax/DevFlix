using DevFlix.Api.DTOs;

namespace DevFlix.Api.Services;

public interface IVideoService
{
    Task<IEnumerable<VideoDto>> GetAllAsync(int? categoryId = null, int? channelId = null);

    Task<VideoDto?> GetByIdAsync(int id);

    Task<VideoDto> CreateAsync(CreateVideoDto dto);

    Task<VideoDto?> UpdateAsync(int id, UpdateVideoDto dto);

    Task<bool> DeleteAsync(int id);
}
