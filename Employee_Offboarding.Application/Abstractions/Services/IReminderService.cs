namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IReminderService
    {
        Task<int> SendRemaindersForOverdueModulesAsync(CancellationToken ct = default);
    }
}
