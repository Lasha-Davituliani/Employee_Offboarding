namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IModuleAssignmentService
    {
        Task<IReadOnlyList<int>> ResolveDepartmentsAsync(int? serviceCentreId, IEnumerable<int> chesenDepartmentIds, CancellationToken ct = default);
        Task<IReadOnlyList<int>> AssignManagersAsync(int formId, IEnumerable<int> departmentIds, CancellationToken ct = default);

    }
}
