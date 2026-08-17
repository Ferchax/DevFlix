using DevFlix.Api.DTOs;
using DevFlix.Api.Services;
using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace DevFlix.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideosController(IVideoService videoService, IValidator<CreateVideoDto> createValidator, IValidator<UpdateVideoDto> updateValidator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoDto>>> GetAll([FromQuery] int? categoryId, [FromQuery] int? channelId)
    {
        var videos = await videoService.GetAllAsync(categoryId, channelId);
        return Ok(videos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VideoDto>> GetById(int id)
    {
        var video = await videoService.GetByIdAsync(id);

        if (video is null)
        {
            return NotFound();
        }

        return Ok(video);
    }

    [HttpPost]
    public async Task<ActionResult<VideoDto>> Create(CreateVideoDto dto)
    {
        var result = await createValidator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return ValidationProblem();
        }

        var video = await videoService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = video.Id }, video);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VideoDto>> Update(int id, UpdateVideoDto dto)
    {
        var result = await updateValidator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return ValidationProblem();
        }

        var video = await videoService.UpdateAsync(id, dto);

        if (video is null)
        {
            return NotFound();
        }

        return Ok(video);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await videoService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
