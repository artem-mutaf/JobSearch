using Microsoft.EntityFrameworkCore;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using System.Linq.Expressions;
using JobBoard.Core.Interfaces;

namespace JobBoard.Infrastructure.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly JobBoardDbContext _context;

    public CategoryRepository(JobBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.Applicants)
            .Include(c => c.Employers)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(
        Expression<Func<Category, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true)
    {
        var query = _context.Categories
            .Include(c => c.Applicants)
            .Include(c => c.Employers)
            .AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                query = ascending ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}