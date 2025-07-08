using System.Threading.Tasks;

namespace JobBoard.Core.Interfaces;

public interface IEmailConfirmationService
{
    Task<string> GenerateConfirmationCodeAsync(string email, Guid userId);
    Task<bool> ConfirmEmailAsync(string code);
}