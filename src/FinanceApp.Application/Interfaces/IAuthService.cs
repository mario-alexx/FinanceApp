using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Common.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(ApplicationUser user, string password);
    Task<string?> LoginAsync(string email, string password);
    Task<bool> ForgotPasswordAsync(string email);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
}
