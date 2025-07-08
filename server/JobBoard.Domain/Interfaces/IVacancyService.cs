using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IVacancyService
{
    Task<Vacancy> CreateVacancyAsync(Vacancy vacancy);
    Task<Vacancy?> GetVacancyByIdAsync(Guid id);
    Task<IEnumerable<Vacancy>> GetAllVacanciesAsync();
    Task UpdateVacancyAsync(Vacancy vacancy);
    Task DeleteVacancyAsync(Guid id);
    Task<IEnumerable<Vacancy>> SearchVacanciesAsync(Guid? categoryId, int pageNumber, int pageSize, string? sortBy, bool sortDescending);
}