using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Application.Abstractions.Services;
using Employee_Offboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class FormService : IFormService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISystemClocks _clocks;
        public FormService(IUnitOfWork uow, ISystemClocks clocks)
        {
            _uow = uow;
            _clocks = clocks;
        }
        public async Task AddItemsAsync(int formId, IEnumerable<int> itemIds, CancellationToken ct = default)
        {
            var ids = itemIds?.Distinct().ToArray() ?? Array.Empty<int>();
            if (ids.Length == 0) throw new ArgumentException("At least one itemId must be provided.");

            var items = await _uow.ItemRepository.Query()
                .Where(i => ids.Contains(i.Id))
                .Select(i => new { i.Id, i.DepartmentId })
                .ToListAsync(ct);
            if(items.Count != ids.Length) throw new ArgumentException("Some itemIds are invalid.");

            var deptIds = items.Select(i => i.DepartmentId).Distinct().ToArray();
            if (deptIds.Any(d => d == 0))
                throw new InvalidOperationException("Some items have DepartmentId = 0 (not set).");

            var existingDeptIds = await _uow.DepartmentRepository.Query()
                .Where(d => deptIds.Contains(d.Id))
                .Select(d => d.Id)
                .ToListAsync(ct);

            var missing = deptIds.Except(existingDeptIds).ToArray();
            if (missing.Length > 0)
                throw new InvalidOperationException($"Missing Departments: {string.Join(", ", missing)}");

            var existing = await _uow.ClearanceFormItemRepository.Query()
           .Where(fi => fi.ClearanceFormId == formId && ids.Contains(fi.ItemId))
           .Select(fi => fi.ItemId)
           .ToListAsync(ct);

            var toInsert = items.Where(i => !existing.Contains(i.Id)).ToList();
            if (toInsert.Count == 0) return;

            foreach (var it in toInsert)
            {
                var row = new ClearenceFormItem
                {
                    ClearanceFormId = formId,
                    ItemId = it.Id,                    
                    DepartmentId = it.DepartmentId,
                    Status = ClearanceFormItemStatus.Pending, 
                    UpdatedAt = DateTime.UtcNow
                };
                await _uow.ClearanceFormItemRepository.AddAsync(row, ct); 
            }

            _uow.SaveChanges();
        }

        public async Task<int> CreateFormAsync(int employeeId, int initiatedByUserId, int? serviceCenterId, IEnumerable<int> departmentIds, CancellationToken ct = default)
        {
            var depIds = departmentIds?.Distinct().ToArray() ?? Array.Empty<int>();
            if(depIds.Length == 0) throw new ArgumentException("At least one departmentId must be provided.");

            var form = new ClearenceForm()
            {
                EmployeeId = employeeId,
                InitiatedByUserId = initiatedByUserId,
                ServiceCenterId = serviceCenterId,
                Status = ClarenceStatus.Pending,
                CreatedAt = _clocks.Now
            };
            await _uow.ClearanceFormRepository.AddAsync(form, ct);
            await _uow.SaveChangesAsync(ct);
            var bridges = depIds.Select(dId => new ClearenceFormDepartment() { ClearenceFormId = form.Id, DepartmentId = dId });
            await _uow.ClearanceFormDepartmentRepository.AddRangeAsync(bridges, ct);
            await _uow.SaveChangesAsync(ct);
            return form.Id;
        }

        public Task<ClearenceForm?> GetAsync(int id, CancellationToken ct = default)
            => _uow.ClearanceFormRepository.GetByIdAsync(id,true, ct);

        public Task<IReadOnlyList<ClearenceForm>> ListAsync(CancellationToken ct = default)
            => _uow.ClearanceFormRepository.GetAllAsync(true, ct);

        public async Task SetItemsStatusAsync(int FormItemId, ClearanceFormItemStatus status, string? textValue, CancellationToken ct = default)
        {
            var fi = await _uow.ClearanceFormItemRepository.GetByIdAsync(FormItemId, false, ct)
                ?? throw new KeyNotFoundException("Form item not found.");

            fi.Status = status;
            fi.TextValue = textValue;
            fi.UpdatedAt = DateTime.UtcNow;

            _uow.ClearanceFormItemRepository.Update(fi);
            _uow.SaveChanges();
        }
    }
}
