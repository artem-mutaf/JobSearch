using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using JobBoard.Application.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Application.Validators;
using JobBoard.Core.Interfaces;
using AutoMapper;

namespace JobBoard.Web.Controllers;

[ApiController]
[Route("api/vacancies")]
public class VacanciesController : ControllerBase
{
    private readonly IVacancyService _vacancyService;
    private readonly IValidator<VacancyDto> _vacancyValidator;
    private readonly IValidator<VacancySearchDto> _searchValidator;
    private readonly IMapper _mapper;

    public VacanciesController(
        IVacancyService vacancyService,
        IValidator<VacancyDto> vacancyValidator,
        IValidator<VacancySearchDto> searchValidator,
        IMapper mapper)
    {
        _vacancyService = vacancyService;
        _vacancyValidator = vacancyValidator;
        _searchValidator = searchValidator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VacancyDto dto)
    {
        try
        {
            var validationResult = await _vacancyValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var vacancy = _mapper.Map<Vacancy>(dto);
            var result = await _vacancyService.CreateVacancyAsync(vacancy);
            var resultDto = _mapper.Map<VacancyDto>(result);
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
        var vacancy = await _vacancyService.GetVacancyByIdAsync(id);
        if (vacancy == null)
            return NotFound();
        var vacancyDto = _mapper.Map<VacancyDto>(vacancy);
        return Ok(vacancyDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vacancies = await _vacancyService.GetAllVacanciesAsync();
        var vacancyDtos = _mapper.Map<IEnumerable<VacancyDto>>(vacancies);
        return Ok(vacancyDtos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VacancyDto dto)
    {
        try
        {
            var validationResult = await _vacancyValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var vacancy = await _vacancyService.GetVacancyByIdAsync(id);
            if (vacancy == null)
                return NotFound();

            _mapper.Map(dto, vacancy); // Обновляем vacancy из dto
            await _vacancyService.UpdateVacancyAsync(vacancy);
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
            await _vacancyService.DeleteVacancyAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] VacancySearchDto dto)
    {
        try
        {
            var validationResult = await _searchValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var vacancies = await _vacancyService.SearchVacanciesAsync(
                dto.CategoryId,
                dto.PageNumber,
                dto.PageSize,
                dto.SortBy,
                dto.SortDescending);
            var vacancyDtos = _mapper.Map<IEnumerable<VacancyDto>>(vacancies);
            return Ok(vacancyDtos);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}