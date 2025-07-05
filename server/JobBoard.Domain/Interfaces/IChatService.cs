using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IChatService
{
    Task<Chat> CreateChatAsync(Chat chat);
    Task<Chat?> GetChatByIdAsync(Guid id);
    Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId);
    Task DeleteChatAsync(Guid id);
}