using System.Linq.Expressions;
using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IApplicantRepository
{
    Task<Applicant?> GetByIdAsync(Guid id);
    Task<Applicant?> GetByEmailAsync(string email);
    Task<IEnumerable<Applicant>> GetAllAsync(
        Expression<Func<Applicant, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10);
    Task AddAsync(Applicant applicant);
    Task UpdateAsync(Applicant applicant);
    Task DeleteAsync(Guid id);
}