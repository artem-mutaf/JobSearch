using System.Linq.Expressions;
using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id);
    Task<IEnumerable<Message>> GetByChatIdAsync(Guid chatId);
    Task<IEnumerable<Message>> GetAllAsync(
        Expression<Func<Message, bool>>? filter = null,
        string? sortBy = null,
        bool ascending = true);
    Task AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task DeleteAsync(Guid id);
}