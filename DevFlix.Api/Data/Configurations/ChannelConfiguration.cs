using DevFlix.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFlix.Api.Data.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable("Channels");

        builder.HasKey(channel => channel.Id);

        builder.Property(channel => channel.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(channel => channel.YouTubeChannelId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(channel => channel.FeedUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(channel => channel.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(channel => channel.YouTubeChannelId)
            .IsUnique();
    }
}
