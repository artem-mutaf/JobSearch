using System.Linq.Expressions;
using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IVacancyRepository
{
    Task<Vacancy?> GetByIdAsync(Guid id);
    Task<IEnumerable<Vacancy>> GetAllAsync(
        Expression<Func<Vacancy, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10);
    Task<IEnumerable<Vacancy>> GetByEmployerIdAsync(Guid employerId);
    Task AddAsync(Vacancy vacancy);
    Task UpdateAsync(Vacancy vacancy);
    Task DeleteAsync(Guid id);
}