using DevFlix.Api.DTOs;
using DevFlix.Api.Services;
using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace DevFlix.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChannelsController(IChannelService channelService, IValidator<CreateChannelDto> createValidator, IValidator<UpdateChannelDto> updateValidator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChannelDto>>> GetAll()
    {
        var channels = await channelService.GetAllAsync();
        return Ok(channels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChannelDto>> GetById(int id)
    {
        var channel = await channelService.GetByIdAsync(id);

        if (channel is null)
        {
            return NotFound();
        }

        return Ok(channel);
    }

    [HttpPost]
    public async Task<ActionResult<ChannelDto>> Create(CreateChannelDto dto)
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

        var channel = await channelService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = channel.Id }, channel);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ChannelDto>> Update(int id, UpdateChannelDto dto)
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

        var channel = await channelService.UpdateAsync(id, dto);

        if (channel is null)
        {
            return NotFound();
        }

        return Ok(channel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await channelService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
