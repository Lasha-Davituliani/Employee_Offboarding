using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IFormService
    {
        Task<int> CreateFormAsync(int employeeId, int initiatedByUserId, int? serviceCenterId, IEnumerable<int> departmentIds, CancellationToken ct = default);
        Task AddItemsAsync(int formId, IEnumerable<int> itemIds, CancellationToken ct = default);
        Task SetItemsStatusAsync(int FormItemId, ClearanceFormItemStatus status, string? textValue, CancellationToken ct = default);
        Task<ClearenceForm?> GetAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<ClearenceForm>> ListAsync(CancellationToken ct = default);
    }
}
