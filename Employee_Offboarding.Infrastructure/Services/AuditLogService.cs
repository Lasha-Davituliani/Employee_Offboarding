using Employee_Offboarding.Application.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ILogger<AuditLogService> _logger;
        public AuditLogService(ILogger<AuditLogService> logger) => _logger = logger;
        public Task LogAsync(string action, string actor, string details, CancellationToken ct = default)
        {
            _logger.LogInformation("Audit Log - Action: {Action}, Actor: {Actor}, Details: {Details}", action, actor, details);
            return Task.CompletedTask;
        }
    }
}
