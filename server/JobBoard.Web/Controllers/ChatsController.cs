using AutoMapper; 
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using JobBoard.Application.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Application.Validators;
using JobBoard.Core.Interfaces;

namespace JobBoard.Web.Controllers;

[ApiController]
[Route("api/chats")]
public class ChatsController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IValidator<ChatDto> _validator;
    private readonly IMapper _mapper; 

    public ChatsController(IChatService chatService, IValidator<ChatDto> validator, IMapper mapper)
    {
        _chatService = chatService;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ChatDto dto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var chat = _mapper.Map<Chat>(dto); 
            var result = await _chatService.CreateChatAsync(chat);
            var resultDto = _mapper.Map<ChatDto>(result); 
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
        var chat = await _chatService.GetChatByIdAsync(id);
        if (chat == null)
            return NotFound();
        var chatDto = _mapper.Map<ChatDto>(chat); 
        return Ok(chatDto);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var chats = await _chatService.GetChatsByUserIdAsync(userId);
        var chatDtos = _mapper.Map<IEnumerable<ChatDto>>(chats);
        return Ok(chatDtos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _chatService.DeleteChatAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}