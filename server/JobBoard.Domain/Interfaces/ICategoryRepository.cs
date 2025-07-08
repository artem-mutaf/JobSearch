using System.Linq.Expressions;
using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllAsync(
        Expression<Func<Category, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Guid id);
}