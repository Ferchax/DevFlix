using DevFlix.Api.Entities;

namespace DevFlix.Api.Repositories;

public interface IChannelRepository
{
    Task<Channel?> GetByIdAsync(int id);
}
