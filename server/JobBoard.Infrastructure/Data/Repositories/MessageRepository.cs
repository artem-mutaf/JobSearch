using Microsoft.EntityFrameworkCore;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using System.Linq.Expressions;
using JobBoard.Core.Interfaces;

namespace JobBoard.Infrastructure.Data.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly JobBoardDbContext _context;

    public MessageRepository(JobBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await _context.Messages
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Message>> GetByChatIdAsync(Guid chatId)
    {
        return await _context.Messages
            .Where(m => m.ChatId == chatId)
            .OrderBy(m => m.SentAt) // Сортировка по времени отправки
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetAllAsync(
        Expression<Func<Message, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true)
    {
        var query = _context.Messages
            .AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortBy.Equals("SentAt", StringComparison.OrdinalIgnoreCase))
                query = ascending ? query.OrderBy(m => m.SentAt) : query.OrderByDescending(m => m.SentAt);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Message message)
    {
        _context.Messages.Update(message);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message != null)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}