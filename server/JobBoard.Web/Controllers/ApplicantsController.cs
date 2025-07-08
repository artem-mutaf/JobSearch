using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using JobBoard.Application.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Application.Validators;
using JobBoard.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Web.Controllers;

[ApiController]
[Route("api/applicants")]
public class ApplicantsController : ControllerBase
{
    private readonly IApplicantService _applicantService;
    private readonly IValidator<ApplicantDto> _validator;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<Applicant> _passwordHasher;

    public ApplicantsController(
        IApplicantService applicantService,
        IValidator<ApplicantDto> validator,
        IMapper mapper,
        IPasswordHasher<Applicant> passwordHasher)
    {
        _applicantService = applicantService;
        _validator = validator;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ApplicantDto dto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var applicant = _mapper.Map<Applicant>(dto);
            applicant.IsEmailConfirmed = false;
            applicant.Password = _passwordHasher.HashPassword(applicant, dto.Password);
            var result = await _applicantService.CreateApplicantAsync(applicant, dto.CategoryIds);
            var resultDto = _mapper.Map<ApplicantDto>(result);
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
        var applicant = await _applicantService.GetApplicantByIdAsync(id);
        if (applicant == null)
            return NotFound();

        var applicantDto = _mapper.Map<ApplicantDto>(applicant);
        return Ok(applicantDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var applicants = await _applicantService.GetAllApplicantsAsync();
        var applicantDtos = _mapper.Map<IEnumerable<ApplicantDto>>(applicants);
        return Ok(applicantDtos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ApplicantDto dto)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var applicant = await _applicantService.GetApplicantByIdAsync(id);
            if (applicant == null)
                return NotFound();

            _mapper.Map(dto, applicant);
            if (!string.IsNullOrEmpty(dto.Password))
            {
                applicant.Password = _passwordHasher.HashPassword(applicant, dto.Password);
            }
            await _applicantService.UpdateApplicantAsync(applicant, dto.CategoryIds);
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
            await _applicantService.DeleteApplicantAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("{applicantId}/respond/{vacancyId}")]
    public async Task<IActionResult> RespondToVacancy(Guid applicantId, Guid vacancyId)
    {
        try
        {
            var chat = await _applicantService.RespondToVacancyAsync(applicantId, vacancyId);
            var chatDto = _mapper.Map<ChatDto>(chat);
            return Ok(chatDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}