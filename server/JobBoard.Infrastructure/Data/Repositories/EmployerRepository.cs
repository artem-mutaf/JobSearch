using Microsoft.EntityFrameworkCore;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using System.Linq.Expressions;
using JobBoard.Core.Interfaces;

namespace JobBoard.Infrastructure.Data.Repositories;

public class EmployerRepository : IEmployerRepository
{
    private readonly JobBoardDbContext _context;

    public EmployerRepository(JobBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Employer?> GetByIdAsync(Guid id)
    {
        return await _context.Employers
            .Include(e => e.Categories)
            .Include(e => e.Vacancies)
            .Include(e => e.Chats)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employer?> GetByEmailAsync(string email)
    {
        return await _context.Employers
            .Include(e => e.Categories)
            .Include(e => e.Vacancies)
            .Include(e => e.Chats)
            .FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<IEnumerable<Employer>> GetAllAsync(
        Expression<Func<Employer, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = _context.Employers
            .Include(e => e.Categories)
            .Include(e => e.Vacancies)
            .Include(e => e.Chats)
            .AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                query = ascending ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email);
            else if (sortBy.Equals("CompanyDescription", StringComparison.OrdinalIgnoreCase))
                query = ascending ? query.OrderBy(e => e.CompanyDescription) : query.OrderByDescending(e => e.CompanyDescription);
        }

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task AddAsync(Employer employer)
    {
        await _context.Employers.AddAsync(employer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employer employer)
    {
        _context.Employers.Update(employer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var employer = await _context.Employers.FindAsync(id);
        if (employer != null)
        {
            _context.Employers.Remove(employer);
            await _context.SaveChangesAsync();
        }
    }
}