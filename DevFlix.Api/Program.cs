using DevFlix.Api.Data;
using DevFlix.Api.DTOs;
using DevFlix.Api.Middleware;
using DevFlix.Api.Repositories;
using DevFlix.Api.Services;
using DevFlix.Api.Validators;

using Microsoft.EntityFrameworkCore;

namespace DevFlix.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddScoped<FluentValidation.IValidator<CreateVideoDto>, Validators.CreateVideoValidator>();
        builder.Services.AddScoped<FluentValidation.IValidator<UpdateVideoDto>, Validators.UpdateVideoValidator>();
        builder.Services.AddScoped<FluentValidation.IValidator<CreateChannelDto>, Validators.CreateChannelValidator>();
        builder.Services.AddScoped<FluentValidation.IValidator<UpdateChannelDto>, Validators.UpdateChannelValidator>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DevFlixDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IVideoRepository, VideoRepository>();
        builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
        builder.Services.AddScoped<IVideoService, VideoService>();
        builder.Services.AddScoped<IChannelService, ChannelService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
