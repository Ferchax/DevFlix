using DevFlix.Api.Data.Configurations;
using DevFlix.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFlix.Api.Data;

public class DevFlixDbContext(DbContextOptions<DevFlixDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Channel> Channels => Set<Channel>();

    public DbSet<Video> Videos => Set<Video>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ChannelConfiguration());
        modelBuilder.ApplyConfiguration(new VideoConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
