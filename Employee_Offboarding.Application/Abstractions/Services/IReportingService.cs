namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IReportingService
    {
        Task<int> CountEmployeesLeftAsync(DateTime fromUtc, DateTime toUtc, int? serviceCenterId = null, int? departmentId = null, CancellationToken ct = default);
        Task<IReadOnlyList<(int DepartmentId, int Count)>> LeftByDepartmentAsync(DateTime fromUtc, DateTime toUtc,CancellationToken ct = default);
        Task<IReadOnlyList<(int ServiceCenterId, int Count)>> LeftByServiceCenterAsync(DateTime fromUtc, DateTime toUtc,CancellationToken ct = default);
    }
}
