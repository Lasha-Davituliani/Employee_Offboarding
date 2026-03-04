namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IAuditLogService
    {
        Task LogAsync(string action, string actor, string details, CancellationToken ct = default);
    }
}
