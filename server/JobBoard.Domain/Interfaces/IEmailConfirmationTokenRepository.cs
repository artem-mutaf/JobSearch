using JobBoard.Core.Entities;
using System.Threading.Tasks;

namespace JobBoard.Core.Interfaces;

public interface IEmailConfirmationTokenRepository
{
    Task<EmailConfirmationToken?> GetByCodeAsync(string code);
    Task AddAsync(EmailConfirmationToken token);
    Task UpdateAsync(EmailConfirmationToken token);
}