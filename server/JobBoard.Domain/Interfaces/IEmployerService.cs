using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;


public interface IEmployerService
{
    Task<Employer> CreateEmployerAsync(Employer employer);
    Task<Employer?> GetEmployerByIdAsync(Guid id);
    Task<IEnumerable<Employer>> GetAllEmployersAsync();
    Task UpdateEmployerAsync(Employer employer);
    Task DeleteEmployerAsync(Guid id);
}