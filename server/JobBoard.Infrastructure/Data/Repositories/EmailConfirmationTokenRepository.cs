using Microsoft.EntityFrameworkCore;
using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;

namespace JobBoard.Infrastructure.Data.Repositories;

public class EmailConfirmationTokenRepository : IEmailConfirmationTokenRepository
{
    private readonly JobBoardDbContext _context;

    public EmailConfirmationTokenRepository(JobBoardDbContext context)
    {
        _context = context;
    }

    public async Task<EmailConfirmationToken?> GetByCodeAsync(string code)
    {
        return await _context.EmailConfirmationTokens.FirstOrDefaultAsync(t => t.Code == code);
    }

    public async Task AddAsync(EmailConfirmationToken token)
    {
        await _context.EmailConfirmationTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EmailConfirmationToken token)
    {
        _context.EmailConfirmationTokens.Update(token);
        await _context.SaveChangesAsync();
    }
}