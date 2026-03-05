using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IDerpartmentService
    {
        Task<IReadOnlyList<Department>> ListAsync(CancellationToken ct = default);
        Task<Department> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(string name, CancellationToken ct = default);
        Task UpdateAsync(int id, string name, bool isActive, CancellationToken ct = default);

    }
}
