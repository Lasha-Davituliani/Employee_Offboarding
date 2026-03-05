namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IExportService
    {
        Task<byte[]> ExportFromsToExcelAsync(DateTime fromUtc, DateTime toUtc, CancellationToken ct = default);
    }
}
