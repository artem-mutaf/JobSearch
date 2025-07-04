using System.Linq.Expressions;
using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IChatRepository
{
    Task<Chat?> GetByIdAsync(Guid id);
    Task<IEnumerable<Chat>> GetByApplicantIdAsync(Guid applicantId);
    Task<IEnumerable<Chat>> GetByEmployerIdAsync(Guid employerId);
    Task<IEnumerable<Chat>> GetAllAsync(
        Expression<Func<Chat, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true);
    Task AddAsync(Chat chat);
    Task UpdateAsync(Chat chat);
    Task DeleteAsync(Guid id);
}