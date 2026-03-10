namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IResponsibleDirectoryService
    {
        Task<int?> GetDepartmentManagerUserIdAsync(int departmentId, CancellationToken ct = default);
        Task<int?> GetServiceCenterManagerUserIdAsync(int serviceCenterId, CancellationToken ct = default);
    }
}
