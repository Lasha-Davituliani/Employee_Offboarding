using Employee_Offboarding.Application.Abstractions.Services;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class ExportService : IExportService
    {
        public Task<byte[]> ExportFromsToExcelAsync(DateTime fromUtc, DateTime toUtc, CancellationToken ct = default) =>
            Task.FromResult(Array.Empty<byte>());
    }
}
