using Microsoft.EntityFrameworkCore;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using System.Linq.Expressions;
using JobBoard.Core.Interfaces;

namespace JobBoard.Infrastructure.Data.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly JobBoardDbContext _context;

    public ChatRepository(JobBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Chat?> GetByIdAsync(Guid id)
    {
        return await _context.Chats
            .Include(c => c.Applicant)
            .Include(c => c.Employer)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Chat>> GetByApplicantIdAsync(Guid applicantId)
    {
        return await _context.Chats
            .Include(c => c.Applicant)
            .Include(c => c.Employer)
            .Include(c => c.Messages)
            .Where(c => c.ApplicantId == applicantId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Chat>> GetByEmployerIdAsync(Guid employerId)
    {
        return await _context.Chats
            .Include(c => c.Applicant)
            .Include(c => c.Employer)
            .Include(c => c.Messages)
            .Where(c => c.EmployerId == employerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Chat>> GetAllAsync(
        Expression<Func<Chat, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true)
    {
        var query = _context.Chats
            .Include(c => c.Applicant)
            .Include(c => c.Employer)
            .Include(c => c.Messages)
            .AsQueryable();

        if (filter != null)
            query = query.Where(filter);
        
        return await query.ToListAsync();
    }

    public async Task AddAsync(Chat chat)
    {
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Chat chat)
    {
        _context.Chats.Update(chat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var chat = await _context.Chats.FindAsync(id);
        if (chat != null)
        {
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }
    }
}