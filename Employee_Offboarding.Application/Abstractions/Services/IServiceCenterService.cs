using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IServiceCenterService
    {
        Task<IReadOnlyList<ServiceCenter>> ListAsync(CancellationToken ct = default);
        Task<ServiceCenter?> GetAsync(int Id, CancellationToken ct = default);
        Task<int> CreateAsync(string name, CancellationToken ct = default);
        Task UpdateAsync(int id, string name, bool isActive, CancellationToken ct = default);
    }
}
