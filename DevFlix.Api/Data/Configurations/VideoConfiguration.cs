using DevFlix.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFlix.Api.Data.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Videos");

        builder.HasKey(video => video.Id);

        builder.Property(video => video.YouTubeVideoId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(video => video.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(video => video.Url)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(video => video.ThumbnailUrl)
            .HasMaxLength(500);

        builder.Property(video => video.PublishedAt)
            .HasColumnType("datetimeoffset")
            .IsRequired();

        builder.HasIndex(video => video.YouTubeVideoId)
            .IsUnique();

        builder.HasIndex(video => new { video.ChannelId, video.PublishedAt });

        builder.HasOne(video => video.Channel)
            .WithMany(channel => channel.Videos)
            .HasForeignKey(video => video.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
