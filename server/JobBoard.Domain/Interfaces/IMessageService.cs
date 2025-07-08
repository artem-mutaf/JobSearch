using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IMessageService
{
    Task<Message> CreateMessageAsync(Message message);
    Task<Message?> GetMessageByIdAsync(Guid id);
    Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId);
    Task DeleteMessageAsync(Guid id);
}