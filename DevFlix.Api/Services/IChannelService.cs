using DevFlix.Api.DTOs;

namespace DevFlix.Api.Services;

public interface IChannelService
{
    Task<IEnumerable<ChannelDto>> GetAllAsync();

    Task<ChannelDto?> GetByIdAsync(int id);

    Task<ChannelDto> CreateAsync(CreateChannelDto dto);

    Task<ChannelDto?> UpdateAsync(int id, UpdateChannelDto dto);

    Task<bool> DeleteAsync(int id);
}
