using System.Linq.Expressions;
using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IEmployerRepository
{
    Task<Employer?> GetByIdAsync(Guid id);
    Task<Employer?> GetByEmailAsync(string email);
    Task<IEnumerable<Employer>> GetAllAsync(
        Expression<Func<Employer, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10);
    Task AddAsync(Employer employer);
    Task UpdateAsync(Employer employer);
    Task DeleteAsync(Guid id);
}