using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface ITamplateService
    {
        Task<IReadOnlyList<Item>> GetItemsForDepartmentAsync(int departmentId, CancellationToken ct = default);
    }
}
