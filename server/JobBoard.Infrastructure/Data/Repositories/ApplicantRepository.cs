using Microsoft.EntityFrameworkCore;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using System.Linq.Expressions;
using JobBoard.Core.Interfaces;

namespace JobBoard.Infrastructure.Data.Repositories;

public class ApplicantRepository : IApplicantRepository
{
    private readonly JobBoardDbContext _context;

    public ApplicantRepository(JobBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Applicant?> GetByIdAsync(Guid id)
    {
        return await _context.Applicants
            .Include(a => a.Categories)
            .Include(a => a.Chats)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Applicant?> GetByEmailAsync(string email)
    {
        return await _context.Applicants
            .Include(a => a.Categories)
            .Include(a => a.Chats)
            .FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task<IEnumerable<Applicant>> GetAllAsync(
        Expression<Func<Applicant, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = _context.Applicants
            .Include(a => a.Categories)
            .Include(a => a.Chats)
            .AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                query = ascending ? query.OrderBy(a => a.Email) : query.OrderByDescending(a => a.Email);
            else if (sortBy.Equals("FullName", StringComparison.OrdinalIgnoreCase))
                query = ascending ? query.OrderBy(a => a.FullName) : query.OrderByDescending(a => a.FullName);
        }

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task AddAsync(Applicant applicant)
    {
        await _context.Applicants.AddAsync(applicant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Applicant applicant)
    {
        _context.Applicants.Update(applicant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var applicant = await _context.Applicants.FindAsync(id);
        if (applicant != null)
        {
            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();
        }
    }
}