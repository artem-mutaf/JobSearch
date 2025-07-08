using Microsoft.EntityFrameworkCore;
using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;
using JobBoard.Infrastructure.Data;

namespace JobBoard.Infrastructure.Data.Repositories;

public class EmailConfirmationTokenRepository : IEmailConfirmationTokenRepository
{
    private readonly JobBoardDbContext _context;

    public EmailConfirmationTokenRepository(JobBoardDbContext context)
    {
        _context = context;
    }

    public async Task<EmailConfirmationToken?> GetByTokenAsync(string token)
    {
        return await _context.EmailConfirmationTokens
            .FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task AddAsync(EmailConfirmationToken token)
    {
        await _context.EmailConfirmationTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var token = await _context.EmailConfirmationTokens.FindAsync(id);
        if (token != null)
        {
            _context.EmailConfirmationTokens.Remove(token);
            await _context.SaveChangesAsync();
        }
    }
}