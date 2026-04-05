using DevFlix.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFlix.Api.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.HasIndex(category => category.Name)
            .IsUnique();

        builder.HasMany(category => category.Channels)
            .WithOne(channel => channel.Category)
            .HasForeignKey(channel => channel.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
