using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using JobBoard.Application.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Application.Validators;
using JobBoard.Core.Interfaces;
using AutoMapper;

namespace JobBoard.Web.Controllers;

[ApiController]
[Route("api/messages")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IValidator<MessageDto> _validator;
    private readonly IMapper _mapper;

    public MessagesController(
        IMessageService messageService,
        IValidator<MessageDto> validator,
        IMapper mapper)
    {
        _messageService = messageService;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MessageDto dto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var message = _mapper.Map<Message>(dto);
            var result = await _messageService.CreateMessageAsync(message);
            var resultDto = _mapper.Map<MessageDto>(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, resultDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var message = await _messageService.GetMessageByIdAsync(id);
        if (message == null)
            return NotFound();
        var messageDto = _mapper.Map<MessageDto>(message);
        return Ok(messageDto);
    }

    [HttpGet("chat/{chatId}")]
    public async Task<IActionResult> GetByChatId(Guid chatId)
    {
        try
        {
            var messages = await _messageService.GetMessagesByChatIdAsync(chatId);
            var messageDtos = _mapper.Map<IEnumerable<MessageDto>>(messages);
            return Ok(messageDtos);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _messageService.DeleteMessageAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}