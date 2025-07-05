using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBoard.Application.Services;

public class ApplicantService : IApplicantService
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IChatRepository _chatRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ApplicantService(
        IApplicantRepository applicantRepository,
        IVacancyRepository vacancyRepository,
        IChatRepository chatRepository,
        ICategoryRepository categoryRepository)
    {
        _applicantRepository = applicantRepository;
        _vacancyRepository = vacancyRepository;
        _chatRepository = chatRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Applicant> CreateApplicantAsync(Applicant applicant, List<Guid> categoryIds)
    {
        if (await _applicantRepository.GetByEmailAsync(applicant.Email) != null)
            throw new Exception("Email already exists.");

        // Инициализируем список категорий
        applicant.Categories = new List<Category>();

        // Загружаем категории по их ID
        foreach (var categoryId in categoryIds)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                throw new Exception($"Category with ID {categoryId} not found.");
            applicant.Categories.Add(category);
        }

        applicant.Id = Guid.NewGuid();
        await _applicantRepository.AddAsync(applicant);
        return applicant;
    }

    public async Task<Applicant?> GetApplicantByIdAsync(Guid id)
    {
        return await _applicantRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Applicant>> GetAllApplicantsAsync()
    {
        return await _applicantRepository.GetAllAsync();
    }

    public async Task UpdateApplicantAsync(Applicant applicant, List<Guid> categoryIds)
    {
        if (await _applicantRepository.GetByIdAsync(applicant.Id) == null)
            throw new Exception("Applicant not found.");

        // Инициализируем список категорий
        applicant.Categories = new List<Category>();

        // Загружаем категории по их ID
        foreach (var categoryId in categoryIds)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                throw new Exception($"Category with ID {categoryId} not found.");
            applicant.Categories.Add(category);
        }

        await _applicantRepository.UpdateAsync(applicant);
    }

    public async Task DeleteApplicantAsync(Guid id)
    {
        if (await _applicantRepository.GetByIdAsync(id) == null)
            throw new Exception("Applicant not found.");

        await _applicantRepository.DeleteAsync(id);
    }

    public async Task<Chat> RespondToVacancyAsync(Guid applicantId, Guid vacancyId)
    {
        var applicant = await _applicantRepository.GetByIdAsync(applicantId);
        if (applicant == null)
            throw new Exception("Applicant not found.");

        var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);
        if (vacancy == null)
            throw new Exception("Vacancy not found.");

        var existingChat = (await _chatRepository.GetAllAsync())
            .FirstOrDefault(c => c.ApplicantId == applicantId && c.VacancyId == vacancyId);
        if (existingChat != null)
            throw new Exception("Chat already exists for this applicant and vacancy.");

        var chat = new Chat
        {
            Id = Guid.NewGuid(),
            ApplicantId = applicantId,
            EmployerId = vacancy.EmployerId,
            VacancyId = vacancyId
        };

        await _chatRepository.AddAsync(chat);
        return chat;
    }
}