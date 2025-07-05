using Microsoft.EntityFrameworkCore;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using System.Linq.Expressions;
using JobBoard.Core.Interfaces;

namespace JobBoard.Infrastructure.Data.Repositories;

public class VacancyRepository : IVacancyRepository
{
    private readonly JobBoardDbContext _context;

    public VacancyRepository(JobBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Vacancy?> GetByIdAsync(Guid id)
    {
        return await _context.Vacancies
            .Include(v => v.Employer)
            .Include(v => v.Category)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<IEnumerable<Vacancy>> GetAllAsync(
        Expression<Func<Vacancy, bool>>? filter = null,
    string? sortBy = null,
    bool ascending = true,
    int pageNumber = 1,
    int pageSize = 10)
{
    var query = _context.Vacancies
        .Include(v => v.Employer)
        .Include(v => v.Category)
        .AsQueryable();

    if (filter != null)
        query = query.Where(filter);

    if (!string.IsNullOrEmpty(sortBy))
    {
        switch (sortBy.ToLower())
        {
            case "title":
                query = ascending ? query.OrderBy(v => v.Title) : query.OrderByDescending(v => v.Title);
                break;
            case "createdat":
                query = ascending ? query.OrderBy(v => v.CreatedAt) : query.OrderByDescending(v => v.CreatedAt);
                break;
            case "salary":
                query = ascending ? query.OrderBy(v => v.Salary) : query.OrderByDescending(v => v.Salary);
                break;
            default:
                break;
        }
    }

    return await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
}

    public async Task<IEnumerable<Vacancy>> GetByEmployerIdAsync(Guid employerId)
    {
        return await _context.Vacancies
            .Include(v => v.Employer)
            .Include(v => v.Category)
            .Where(v => v.EmployerId == employerId)
            .ToListAsync();
    }

    public async Task AddAsync(Vacancy vacancy)
    {
        await _context.Vacancies.AddAsync(vacancy);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vacancy vacancy)
    {
        _context.Vacancies.Update(vacancy);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);
        if (vacancy != null)
        {
            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
        }
    }
}