using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<User?> FindByUsernameAsync(string username, CancellationToken ct = default);
        Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
        Task<User> RegisterAsync(string username, string email, string password, CancellationToken ct = default);
        Task<User?> AuthenticateAsync(string usernameOrEmail, string password, CancellationToken ct = default);
        Task RequestPasswordResetAsync(string email, CancellationToken ct = default);
        Task<bool> ResetPasswordAsync(Guid token, string newPassword, CancellationToken ct = default);
    }
}
