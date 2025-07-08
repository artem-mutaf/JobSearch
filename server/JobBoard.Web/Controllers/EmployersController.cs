using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using JobBoard.Application.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Application.Validators;
using JobBoard.Core.Interfaces;
using Microsoft.AspNetCore.Identity; // Для хеширования пароля

namespace JobBoard.Web.Controllers;

[ApiController]
[Route("api/employers")]
public class EmployersController : ControllerBase
{
    private readonly IEmployerService _employerService;
    private readonly IValidator<EmployerDto> _validator;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<Employer> _passwordHasher; // Для хеширования пароля

    public EmployersController(
        IEmployerService employerService,
        IValidator<EmployerDto> validator,
        IMapper mapper,
        IPasswordHasher<Employer> passwordHasher)
    {
        _employerService = employerService;
        _validator = validator;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployerDto dto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var employer = _mapper.Map<Employer>(dto);
            employer.IsEmailConfirmed = false; // Устанавливаем значение по умолчанию
            employer.Password = _passwordHasher.HashPassword(employer, dto.Password); // Хешируем пароль
            var result = await _employerService.CreateEmployerAsync(employer);
            var resultDto = _mapper.Map<EmployerDto>(result);
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
        var employer = await _employerService.GetEmployerByIdAsync(id);
        if (employer == null)
            return NotFound();
        var employerDto = _mapper.Map<EmployerDto>(employer);
        return Ok(employerDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employers = await _employerService.GetAllEmployersAsync();
        var employerDtos = _mapper.Map<IEnumerable<EmployerDto>>(employers);
        return Ok(employerDtos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployerDto dto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var employer = await _employerService.GetEmployerByIdAsync(id);
            if (employer == null)
                return NotFound();

            _mapper.Map(dto, employer); // Обновляем employer из dto
            employer.Password = _passwordHasher.HashPassword(employer, dto.Password); // Хешируем новый пароль
            await _employerService.UpdateEmployerAsync(employer);
            return NoContent();
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
            await _employerService.DeleteEmployerAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}