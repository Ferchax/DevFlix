using DevFlix.Api.Data;
using DevFlix.Api.Entities;

namespace DevFlix.Api.Repositories;

public class ChannelRepository(DevFlixDbContext context) : IChannelRepository
{
    public async Task<Channel?> GetByIdAsync(int id)
    {
        return await context.Channels.FindAsync(id);
    }
}
