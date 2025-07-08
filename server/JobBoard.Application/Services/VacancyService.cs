using System.Linq.Expressions;
using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Application.Services;

public class VacancyService : IVacancyService
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IEmployerRepository _employerRepository;

    public VacancyService(
        IVacancyRepository vacancyRepository,
        ICategoryRepository categoryRepository,
        IEmployerRepository employerRepository)
    {
        _vacancyRepository = vacancyRepository;
        _categoryRepository = categoryRepository;
        _employerRepository = employerRepository;
    }

    public async Task<Vacancy> CreateVacancyAsync(Vacancy vacancy)
    {
        if (await _categoryRepository.GetByIdAsync(vacancy.CategoryId) == null)
            throw new Exception("Category not found.");
        if (await _employerRepository.GetByIdAsync(vacancy.EmployerId) == null)
            throw new Exception("Employer not found.");

        vacancy.Id = Guid.NewGuid();
        vacancy.CreatedAt = DateTime.UtcNow;
        await _vacancyRepository.AddAsync(vacancy);
        return vacancy;
    }

    public async Task<Vacancy?> GetVacancyByIdAsync(Guid id)
    {
        return await _vacancyRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Vacancy>> GetAllVacanciesAsync()
    {
        return await _vacancyRepository.GetAllAsync();
    }

    public async Task UpdateVacancyAsync(Vacancy vacancy)
    {
        if (await _vacancyRepository.GetByIdAsync(vacancy.Id) == null)
            throw new Exception("Vacancy not found.");
        if (await _categoryRepository.GetByIdAsync(vacancy.CategoryId) == null)
            throw new Exception("Category not found.");
        if (await _employerRepository.GetByIdAsync(vacancy.EmployerId) == null)
            throw new Exception("Employer not found.");

        await _vacancyRepository.UpdateAsync(vacancy);
    }

    public async Task DeleteVacancyAsync(Guid id)
    {
        if (await _vacancyRepository.GetByIdAsync(id) == null)
            throw new Exception("Vacancy not found.");

        await _vacancyRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Vacancy>> SearchVacanciesAsync(Guid? categoryId, int pageNumber, int pageSize, string? sortBy, bool sortDescending)
    {
        Expression<Func<Vacancy, bool>>? filter = categoryId.HasValue
            ? v => v.CategoryId == categoryId.Value
            : null;

        return await _vacancyRepository.GetAllAsync(
            filter: filter,
            sortBy: sortBy,
            ascending: !sortDescending,
            pageNumber: pageNumber,
            pageSize: pageSize);
    }
}