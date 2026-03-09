using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IItemService
    {
        Task<IReadOnlyList<Item>> ListByDepartmentAsync(int departmentId, CancellationToken ct = default);
        Task<int> CreateAsync(int departmentId, string name, ItemKind kind, bool isActive, int displayOrder, CancellationToken ct = default);
        Task UpdateAsync(int id, string name, ItemKind kind, bool isActive, int displayOrder, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
