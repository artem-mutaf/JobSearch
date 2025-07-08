using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;

namespace JobBoard.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Chat> CreateChatAsync(Chat chat)
    {
        chat.Id = Guid.NewGuid();
        await _chatRepository.AddAsync(chat);
        return chat;
    }

    public async Task<Chat?> GetChatByIdAsync(Guid id)
    {
        return await _chatRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId)
    {
        return (await _chatRepository.GetAllAsync())
            .Where(c => c.ApplicantId == userId || c.EmployerId == userId);
    }

    public async Task DeleteChatAsync(Guid id)
    {
        if (await _chatRepository.GetByIdAsync(id) == null)
            throw new Exception("Chat not found.");

        await _chatRepository.DeleteAsync(id);
    }
}