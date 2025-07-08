using Microsoft.AspNetCore.Mvc;
using JobBoard.Application.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Web.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IApplicantService _applicantService;
    private readonly IEmployerService _employerService;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IEmployerRepository _employerRepository;
    private readonly IEmailConfirmationService _emailConfirmationService;
    private readonly IValidator<RegisterDto> _registerValidator;
    private readonly IValidator<LoginDto> _loginValidator;
    private readonly IValidator<ConfirmEmailDto> _confirmEmailValidator;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<Applicant> _applicantPasswordHasher;
    private readonly IPasswordHasher<Employer> _employerPasswordHasher;

    public AuthController(
        IApplicantService applicantService,
        IEmployerService employerService,
        IApplicantRepository applicantRepository,
        IEmployerRepository employerRepository,
        IEmailConfirmationService emailConfirmationService,
        IValidator<RegisterDto> registerValidator,
        IValidator<LoginDto> loginValidator,
        IValidator<ConfirmEmailDto> confirmEmailValidator,
        IMapper mapper,
        IPasswordHasher<Applicant> applicantPasswordHasher,
        IPasswordHasher<Employer> employerPasswordHasher)
    {
        _applicantService = applicantService;
        _employerService = employerService;
        _applicantRepository = applicantRepository;
        _employerRepository = employerRepository;
        _emailConfirmationService = emailConfirmationService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _confirmEmailValidator = confirmEmailValidator;
        _mapper = mapper;
        _applicantPasswordHasher = applicantPasswordHasher;
        _employerPasswordHasher = employerPasswordHasher;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var validationResult = await _registerValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            if (dto.Role == "Applicant")
            {
                var applicant = _mapper.Map<Applicant>(dto.Applicant);
                applicant.IsEmailConfirmed = false;
                applicant.Password = _applicantPasswordHasher.HashPassword(applicant, dto.Applicant.Password);
                var result = await _applicantService.CreateApplicantAsync(applicant, dto.Applicant.CategoryIds);
                await _emailConfirmationService.GenerateConfirmationCodeAsync(applicant.Email, applicant.Id);
                return CreatedAtAction(nameof(ApplicantsController.GetById), "Applicants", new { id = result.Id }, _mapper.Map<ApplicantDto>(result));
            }
            else if (dto.Role == "Employer")
            {
                var employer = _mapper.Map<Employer>(dto.Employer);
                employer.IsEmailConfirmed = false;
                employer.Password = _employerPasswordHasher.HashPassword(employer, dto.Employer.Password);
                var result = await _employerService.CreateEmployerAsync(employer);
                await _emailConfirmationService.GenerateConfirmationCodeAsync(employer.Email, employer.Id);
                return CreatedAtAction(nameof(EmployersController.GetById), "Employers", new { id = result.Id }, _mapper.Map<EmployerDto>(result));
            }

            return BadRequest(new { Message = "Недопустимая роль." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto dto)
    {
        try
        {
            var validationResult = await _confirmEmailValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var result = await _emailConfirmationService.ConfirmEmailAsync(dto.Code);
            if (!result)
                return BadRequest(new { Message = "Недействительный или истекший код." });

            return Ok(new { Message = "Почта успешно подтверждена." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var validationResult = await _loginValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            if (dto.Role == "Applicant")
            {
                var applicant = await _applicantRepository.GetByEmailAsync(dto.Email);
                if (applicant == null || !applicant.IsEmailConfirmed)
                    return Unauthorized(new { Message = "Недействительные учетные данные или почта не подтверждена." });

                var passwordVerificationResult = _applicantPasswordHasher.VerifyHashedPassword(applicant, applicant.Password, dto.Password);
                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized(new { Message = "Недействительные учетные данные." });

                return Ok(new { UserId = applicant.Id, Role = "Applicant" });
            }
            else if (dto.Role == "Employer")
            {
                var employer = await _employerRepository.GetByEmailAsync(dto.Email);
                if (employer == null || !employer.IsEmailConfirmed)
                    return Unauthorized(new { Message = "Недействительные учетные данные или почта не подтверждена." });

                var passwordVerificationResult = _employerPasswordHasher.VerifyHashedPassword(employer, employer.Password, dto.Password);
                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    return Unauthorized(new { Message = "Недействительные учетные данные." });

                return Ok(new { UserId = employer.Id, Role = "Employer" });
            }

            return BadRequest(new { Message = "Недопустимая роль." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}