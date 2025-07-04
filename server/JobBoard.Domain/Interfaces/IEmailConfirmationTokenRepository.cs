using JobBoard.Core.Entities;

namespace JobBoard.Core.Interfaces;

public interface IEmailConfirmationTokenRepository
{
    Task<EmailConfirmationToken?> GetByTokenAsync(string token);
    Task AddAsync(EmailConfirmationToken token);
    Task DeleteAsync(Guid id);
}