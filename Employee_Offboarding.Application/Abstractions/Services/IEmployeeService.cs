using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> GetAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Employee>> ListAsync(CancellationToken ct = default);
        Task<int> CreateAsync(string firstName, string lastName, string email, string personlNumber, string? positionTitle, int? departmentId, int? serviceCenterId, string directManagerName, string directManagerEmail, CancellationToken ct = default);
        Task UpdateAsync(int id, string firstName, string lastName, string personalNumber, int? departmentId, int? serviceCenterId, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
