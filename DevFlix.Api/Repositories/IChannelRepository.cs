using DevFlix.Api.DTOs;
using DevFlix.Api.Entities;

namespace DevFlix.Api.Repositories;

public interface IChannelRepository
{
    Task<IEnumerable<ChannelDto>> GetAllAsync();

    Task<Channel?> GetByIdAsync(int id);

    Task<ChannelDto?> GetByIdWithCategoryAsync(int id);

    Task AddAsync(Channel channel);

    Task UpdateAsync(Channel channel);

    Task RemoveAsync(Channel channel);

    Task<bool> CategoryExistsAsync(int categoryId);
}
