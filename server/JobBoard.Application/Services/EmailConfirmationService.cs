using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JobBoard.Application.Services;

public class EmailConfirmationService : IEmailConfirmationService
{
    private readonly IEmailConfirmationTokenRepository _tokenRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IEmployerRepository _employerRepository;
    private readonly IEmailService _emailService;

    public EmailConfirmationService(
        IEmailConfirmationTokenRepository tokenRepository,
        IApplicantRepository applicantRepository,
        IEmployerRepository employerRepository,
        IEmailService emailService)
    {
        _tokenRepository = tokenRepository;
        _applicantRepository = applicantRepository;
        _employerRepository = employerRepository;
        _emailService = emailService;
    }

    public async Task<string> GenerateConfirmationCodeAsync(string email, Guid userId)
    {
        // Генерация 6-значного кода
        var code = new Random().Next(100000, 999999).ToString();

        var confirmationToken = new EmailConfirmationToken
        {
            Id = Guid.NewGuid(),
            Email = email,
            Code = code,
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            IsUsed = false
        };

        await _tokenRepository.AddAsync(confirmationToken);

        var emailBody = $"<p>Ваш код подтверждения: <strong>{code}</strong></p><p>Введите этот код на сайте для подтверждения вашей почты.</p>";
        await _emailService.SendEmailAsync(email, "Подтверждение электронной почты", emailBody);

        return code;
    }

    public async Task<bool> ConfirmEmailAsync(string code)
    {
        var confirmationToken = await _tokenRepository.GetByCodeAsync(code);
        if (confirmationToken == null || confirmationToken.IsUsed || confirmationToken.ExpiresAt < DateTime.UtcNow)
            return false;

        var applicant = await _applicantRepository.GetByIdAsync(confirmationToken.UserId);
        if (applicant != null)
        {
            applicant.IsEmailConfirmed = true;
            await _applicantRepository.UpdateAsync(applicant);
        }
        else
        {
            var employer = await _employerRepository.GetByIdAsync(confirmationToken.UserId);
            if (employer != null)
            {
                employer.IsEmailConfirmed = true;
                await _employerRepository.UpdateAsync(employer);
            }
            else
            {
                return false;
            }
        }

        confirmationToken.IsUsed = true;
        await _tokenRepository.UpdateAsync(confirmationToken);
        return true;
    }
}