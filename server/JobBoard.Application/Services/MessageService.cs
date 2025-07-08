using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;

namespace JobBoard.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;

    public MessageService(IMessageRepository messageRepository, IChatRepository chatRepository)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
    }

    public async Task<Message> CreateMessageAsync(Message message)
    {
        var chat = await _chatRepository.GetByIdAsync(message.ChatId);
        if (chat == null)
            throw new Exception("Chat not found.");

        // Проверка, что SenderId является участником чата (ApplicantId или EmployerId)
        if (message.SenderId != chat.ApplicantId && message.SenderId != chat.EmployerId)
            throw new Exception("Sender is not a participant of the chat.");

        message.Id = Guid.NewGuid();
        message.SentAt = DateTime.UtcNow;
        await _messageRepository.AddAsync(message);
        return message;
    }

    public async Task<Message?> GetMessageByIdAsync(Guid id)
    {
        return await _messageRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        if (chat == null)
            throw new Exception("Chat not found.");

        return await _messageRepository.GetByChatIdAsync(chatId);
    }

    public async Task DeleteMessageAsync(Guid id)
    {
        var message = await _messageRepository.GetByIdAsync(id);
        if (message == null)
            throw new Exception("Message not found.");

        await _messageRepository.DeleteAsync(id);
    }
}